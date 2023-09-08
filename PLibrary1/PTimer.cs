// Predator3
// PLibrary1
// PTimer.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 0:54 08 09 2023

namespace PLibrary1;

public class PTimer
{
    public double Start { get; set; } = 0;
    public double End { get; set; } = 1200;
    public double Step { get; set; } = 1;

    public double Time { get; set; } = 0;
    public double Tick { get; set; } = 0.01;

    public double Run()
    {
        if (Time <= End)
        {
            Time = Time+Step;
        }
        return Time;
    }

    public double RunN(int n = 1)
    {
        for (int i = 0; i < n; i++)
        {
            Run();
        }
        return Time;
    }
    public double Reset() { Time  = 0; return Time; }
}