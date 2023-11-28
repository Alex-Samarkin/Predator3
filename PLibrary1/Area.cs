// Predator3
// PLibrary1
// Area.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 1:50 11 09 2023

using System.ComponentModel;

namespace PLibrary1;

public class Area
{
    public string Name = "Pskov1";
    public int Peoples { get; set; } = 40000;
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("Генератор"), Description("Генератор населения на выбранной площадке. Настроен на Псковскую область.")]
    public AgentGenerator Generator { get; set; } = new AgentGenerator();
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("Площадка"), Description("Участок региона, на котором происходит моделирование.")]
    public RArea RArea { get; set; } = new RArea();
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("Население"), Description("Моделируется возраст, пол и поправочные коэффициенты нв заболеваемост вирусом")]
    public Agents Agents { get; set; } = new Agents();
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [DisplayName("Вирус"), Description("Основные параметры вирусной инфекции. Моделируется усредненный COVID-19")]
    public Virus Virus { get; set; } = new Virus();

    public void Init(int Seed, double W, double H, double FillRatio = 0.1, int peoples = 40000, int InfectedAtStart = 40, int Vaccinate = 0)
    {
        Peoples = peoples;

        RArea = new RArea();
        RArea.W = W;
        RArea.H = H;
        RArea.ResetField();
        RArea.RandomFill((int)(W * H * FillRatio));

        Generator.ResetRng(Seed);
        Generator.Xmax = RArea.W;
        Generator.Ymax = RArea.H;
        Generator.vrs = Virus;

        Agents.FillWithGenerator(Peoples, Generator, true);
        Agents.InfectNPeoples(InfectedAtStart);
        Agents.VaccinateNPeoples(Vaccinate);

        CountOfSuspected.Clear();
        CountOfExposed.Clear();
        CountOfInfected.Clear();
        CountOfRecovered.Clear();
        CountOfDead.Clear();
        CountOfVaccinated.Clear();

        CountOfSuspected.Add(Agents.CountOfSuspected);
        CountOfExposed.Add(Agents.CountOfExposed);
        CountOfInfected.Add(Agents.CountOfInfected);
        CountOfRecovered.Add(Agents.CountOfRecovered);
        CountOfDead.Add(Agents.CountOfDead);
        CountOfVaccinated.Add(Agents.CountOfVaccinated);
    }

    public void Reset()
    {

        RArea.ResetField();
        RArea.RandomFill((int)(RArea.W * RArea.H * 0.1));

        Generator.ResetRng();
        Generator.Xmax = RArea.W;
        Generator.Ymax = RArea.H;
        Generator.vrs = Virus;

        Agents.FillWithGenerator(Peoples, Generator, true);
        Agents.InfectNPeoples(40);
        Agents.VaccinateNPeoples(0);

        CountOfSuspected.Clear();
        CountOfExposed.Clear();
        CountOfInfected.Clear();
        CountOfRecovered.Clear();
        CountOfDead.Clear();
        CountOfVaccinated.Clear();

        CountOfSuspected.Add(Agents.CountOfSuspected);
        CountOfExposed.Add(Agents.CountOfExposed);
        CountOfInfected.Add(Agents.CountOfInfected);
        CountOfRecovered.Add(Agents.CountOfRecovered);
        CountOfDead.Add(Agents.CountOfDead);
        CountOfVaccinated.Add(Agents.CountOfVaccinated);

    }

    public void Run1()
    {
        RArea.Run(false);
        Agents.Run(RArea, false);

        CountOfSuspected.Add(Agents.CountOfSuspected);
        CountOfExposed.Add(Agents.CountOfExposed);
        CountOfInfected.Add(Agents.CountOfInfected);
        CountOfRecovered.Add(Agents.CountOfRecovered);
        CountOfDead.Add(Agents.CountOfDead);
        CountOfVaccinated.Add(Agents.CountOfVaccinated);

    }

    public void RunN(int N = 7)
    {
        for (int i = 0; i < N; i++)
        {
            RArea.Run(false);
            Agents.Run(RArea, false); //!!!
        }
        RArea.Plot();
        Agents.Plot(RArea);
    }

    ///TODO Area Exchange Agents - Area

    ///TODO Async version of

    ///TODO Мониторы и статистика
    public List<int> CountOfSuspected { get; set; } = new List<int>();
    public List<int> CountOfExposed { get; set; } = new List<int>();
    public List<int> CountOfInfected { get; set; } = new List<int>();
    public List<int> CountOfRecovered { get; set; } = new List<int>();
    public List<int> CountOfDead { get; set; } = new List<int>();
    public List<int> CountOfVaccinated { get; set; } = new List<int>();

    public enum CovidStateMonitor { Suspected=0, Exposed, Infected, Recovered, Dead, Vaccinated };

    public List<int> ListByState(CovidStateMonitor state)
    {
        switch (state)
        {
            case CovidStateMonitor.Suspected: return CountOfSuspected;
            case CovidStateMonitor.Exposed: return CountOfExposed;
            case CovidStateMonitor.Infected: return CountOfInfected;
            case CovidStateMonitor.Recovered: return CountOfRecovered;
            case CovidStateMonitor.Dead: return CountOfDead;
            case CovidStateMonitor.Vaccinated: return CountOfVaccinated;
            default: return new List<int>();
        }
    }
}