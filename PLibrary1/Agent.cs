// Predator3
// PLibrary1
// Agent.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 1:54 09 09 2023

using System.ComponentModel;

namespace PLibrary1;

/// <summary>
/// статус агента - подвержен, в инкубационном периоде, инфицирован, выздоровел, мертв, вакцинирован
/// </summary>
public enum CovidState
{
    Suspected = 0,
    Exposed = 1,
    Infected = 2,
    Recovered = 3,
    Dead = 4,
    Vaccinated = 5
}
/// <summary>
/// статус здоровья - здоров, легкие или средние последствия, тяжелые последствия 
/// </summary>
public enum HealState
{
    Ok = 0,
    Moderate = 1,
    Hard= 2,
}

public enum GenderState
{
    Male = 1,
    Female = 0,
}


/// <summary>
/// отдельный индивидуум
/// </summary>
public class Agent
{
    /// <summary>
    /// условный идентификатор
    /// </summary>
    public int Id { get; set; } = 0;

    /// <summary>
    /// номер агента в структурах типа вектора
    /// </summary>
    public int Index { get; set; } = 0;

    /// <summary>
    /// координата х
    /// </summary>
    public double x { get; set; } = 0;
    /// <summary>
    /// координата y
    /// </summary>
    public double y { get; set; } = 0; 
    /// <summary>
    /// вспомогательная величина, резерв
    /// </summary>
    public double z { get; set; } = 0;

    /// <summary>
    /// скорость движения
    /// </summary>
    public double v { get; set; } = 0;
    /// <summary>
    /// ускорение
    /// </summary>
    public double a { get; set; } = 0;
    /// <summary>
    /// угол к оси X скорости
    /// </summary>
    public double angle { get; set; } = 0;
    /// <summary>
    /// возраст
    /// </summary>
    public double Age { get; set; } = 30;
    /// <summary>
    /// пол
    /// </summary>
    public GenderState Gender { get; set; } = GenderState.Male;
    /// <summary>
    /// поправочные коэффициенты индивидуума на течение болезни
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public Koeff Koeff { get; set; } = new Koeff(){Kinf = 1, Kdead = 0, Ke = 0, Khard = 1,Ki=1};

    public CovidState State { get; set; } = CovidState.Suspected;
    public HealState Heal { get; set; } = HealState.Ok;
    /// <summary>
    /// моделируется движение агента
    /// </summary>
    /// <param name="da"></param>
    /// <param name="dangle"></param>
    public void Run(double da = 0, double dangle = 0)
    {
        x = x + v * Math.Cos(angle);
        y = y + v * Math.Sin(angle);

        v = v + a;

        a = a + da;

        angle = angle + dangle;
        /// Normalize angle to -pi +pi
        angle = angle - 2 * Math.PI * Math.Floor(angle / (2 * Math.PI));
    }

    public double TimeFromStart { get; set; } = 0;
    /// <summary>
    /// проверка расстояния
    /// </summary>
    /// <param name="other_x"> координата X</param>
    /// <param name="other_y"> координата Y</param>
    /// <param name="dist"> расстояние, которое является критерием близости</param>
    /// <returns></returns>
    public bool IsNear(double other_x, double other_y, double dist)
    {
        var dx = x - other_x;
        var dy = y - other_y;

        if (Math.Abs(dx) > dist) return false;
        if (Math.Abs(dy) > dist) return false;

        var r2 = dx*dx + dy * dy;
        
        if (r2 < dist*dist ) return true;

        return false;
    }
    /// <summary>
    /// ускоренная проверка близости
    /// проверяется попаадание в квадрат
    /// </summary>
    /// <param name="other_x"></param>
    /// <param name="other_y"></param>
    /// <param name="dist"></param>
    /// <returns></returns>
    public bool IsNearQ(double other_x, double other_y, double dist)
    {
        var dx = x - other_x;
        var dy = y - other_y;

        if (Math.Abs(dx) > dist) return false;
        if (Math.Abs(dy) > dist) return false;
        
        return true;
    }
    public bool IsNear(Agent other, double dist)
    {
        var dx = x - other.x;
        var dy = y - other.y;

        if (Math.Abs(dx) > dist) return false;
        if (Math.Abs(dy) > dist) return false;

        var r2 = dx * dx + dy * dy;

        if (r2 < dist * dist) return true;

        return false;
    }
    public bool IsNearQ(Agent other, double dist)
    {
        var dx = x - other.x;
        var dy = y - other.y;

        if (Math.Abs(dx) > dist) return false;
        if (Math.Abs(dy) > dist) return false;

       return true;
    }

    // TODO Добавить проверку контакта с инфицированным

    // TODO добавить проверку земли на предмет усиления действия вируса
}