// Predator3
// PLibrary1
// Region.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 3:11 11 09 2023

using System.Diagnostics;
using System.Text;

namespace PLibrary1;

public class Region
{
    public string Name { get; set; } = "Region60";
    public List<Area> Areas { get; set; } = new List<Area>();
    public PTimer Timer { get; set; } = new PTimer();

    public void Init(int AreaCount = 5)
    {
        for (int i = 0; i < AreaCount; i++)
        {
            Area area = new Area();
            area.Name = $"Area {i}";
            var Rn = new Random();
            area.Init( Rn.Next(), Rn.Next(400,1000), Rn.Next(400,1000), Rn.NextDouble()/10.0, Rn.Next(20000,60000), Rn.Next(25,200),0);
            Areas.Add(area);
        }
    }

    public void Run1()
    {
        Timer.Run();
        foreach (Area area in Areas)
        {
            Trace.WriteLine(area.Name);
            area.Run1();
        }
    }
    public int Run1t()
    {
        Timer.Run();
        foreach (Area area in Areas)
        {
            Trace.WriteLine(area.Name);
            area.Run1();
        }
        return (int)Timer.Time;
    }
    public void RunN(int N = 7)
    {
        //Timer.RunN(N)
        for (int i = 0; i < N; i++)
        {
            Run1();
        }
    }

    public void RunAll()
    {
        while (Timer.Time<=Timer.End)
        {
            Run1();
        }
    }

    public async Task RunAsync()
    {
        while (Timer.Time <= Timer.End)
        {
            Trace.WriteLine(Timer.Time);
            await Task.Run(Run1);
        }
    }

    ///TODO
    /// добавить мониторы и статистику
    public enum CovidStateMonitor { Suspected = 0, Exposed, Infected, Recovered, Dead, Vaccinated };
    public List<int> ListByState(CovidStateMonitor state, int AreaIndex = -1)
    {
        // если индкс -1, то суммируем, иначе выбираем область по индексу
        // проверяем индекс
        if (AreaIndex>=Areas.Count) AreaIndex = -1;
        // результат
        List<int> res = new List<int>();

        // если индекс области соответствует области, выдаем ее данные
        if (AreaIndex != -1)
        {
            res = Areas[AreaIndex].ListByState((Area.CovidStateMonitor)(int)state);
        }
        // иначе считаем сумму
        else
        {
            try
            {
                // делаем КОПИЮ первой области 
                res.AddRange(Areas[0].ListByState((Area.CovidStateMonitor)(int)state).ToArray());
                for (int i = 1; i < Areas.Count; i++)
                {
                    // суммируем к результату данные по остальным областям
                    for (int j = 0; j < res.Count; j++)
                    {
                        res[j] += Areas[i].ListByState((Area.CovidStateMonitor)(int)state)[j];
                    }
                }
            }
            catch
            {
                res = new List<int>();
            }
        }
        return res;
    }
    public List<int> Suspected(int AreaIndex = -1) => ListByState(CovidStateMonitor.Suspected, AreaIndex);
    public List<int> Exposed(int AreaIndex = -1) => ListByState(CovidStateMonitor.Exposed, AreaIndex);
    public List<int> Infected(int AreaIndex = -1) => ListByState(CovidStateMonitor.Infected, AreaIndex);
    public List<int> Recovered(int AreaIndex = -1) => ListByState(CovidStateMonitor.Recovered, AreaIndex);
    public List<int> Dead(int AreaIndex = -1) => ListByState(CovidStateMonitor.Dead, AreaIndex);

    public string Monitor()
    {
        int last = Suspected().Count - 1;    
        return $"time {Timer.Time}/S {Suspected()[last]}/E {Exposed()[last]}/I {Infected()[last]}/R {Recovered()[last]}/D {Dead()[last]}";
    }

    public string ToCSV()
    {
        var sb = new StringBuilder("T");
        foreach (Area area in Areas)
        {
            sb.Append($",{area.Name},S,E,I,R,D,");
        }
        sb.AppendLine("ALL,S,E,I,R,D,");
        for (int i = 0; i < Areas[0].CountOfSuspected.Count; i++)
        {
            sb.Append($"{i}");
            int s=0, e = 0, inf = 0, r = 0, d = 0;
            foreach (Area area in Areas)
            {
                int s1 = 0, e1 = 0, inf1 = 0, r1 = 0, d1 = 0;
                s1 = area.CountOfSuspected[i];
                e1 = area.CountOfExposed[i];
                inf1 = area.CountOfInfected[i];
                r1 = area.CountOfRecovered[i];
                d1 = area.CountOfDead[i];

                sb.Append($",{area.Name},{s1},{e1},{inf1},{r1},{d1},");

                s += s1;
                e += e1;
                inf += inf1;
                r += r1;
                d += d1;
            }
            sb.AppendLine($"All,{s},{e},{inf},{r},{d}");
        }
        return sb.ToString();

    }
    /// добавить графики
}