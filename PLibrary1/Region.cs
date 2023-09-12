// Predator3
// PLibrary1
// Region.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 3:11 11 09 2023

using System.Diagnostics;

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
    /// 
}