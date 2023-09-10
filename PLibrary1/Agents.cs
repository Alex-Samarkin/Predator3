// Predator3
// PLibrary1
// Agents.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 1:38 10 09 2023

using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

using System.Drawing;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using MathNet.Numerics.Statistics;
using System;

namespace PLibrary1;

public class Agents
{
    public List<Agent> Items { get; set; } =new List<Agent>();
    public int Count => Items.Count;

    public double MaxDa { get; set; } = 0.1;
    public double MaxDAngle { get; set; } = 30.0 * Math.PI / 180.0;
    public double Maxa { get; set; } = 1;
    public double MaxV { get; set; } = 5;

    public int Seed { get; set; } = 42;
    public RandomSource Rnd { get; set; } = new MersenneTwister();

    public void FillWithGenerator(int NewAgentsCount, AgentGenerator generator, bool ClearOld = true)
    {
        generator ??= new AgentGenerator();
        if (ClearOld ==true)
            Items.Clear();
        
        generator.ResetRng();

        for (int i = 0; i < NewAgentsCount; i++)
        {
            Items.Add(generator.BuildNewAgent());
        }
    }

    public void InfectNPeoples(int N)
    {
        var Indexes = new SortedSet<int>();

        while (Indexes.Count < N)
        {
            Indexes.Add(Rnd.Next(Items.Count));
        }

        foreach (int index in Indexes)
        {
            Items[index].SetState(CovidState.Infected);
        }
    }
    
    public void Clear() { Items.Clear(); }

    public void Run(RArea r1)
    {
        Move();
        UpdateVA(r1);

        Plot(r1);
    }

    //01 Переместить всех агентов
    public void Move()
    {
        foreach (Agent agent in Items)
        {
            agent.Run(Normal.Sample(Rnd,0,MaxDa),Normal.Sample(0,MaxDAngle));
        }
    }

    ///02 Проверить скорости, ускорения и координаты
    public void UpdateVA(RArea r1)
    {
        foreach (Agent agent in Items)
        {
            if (agent.v > MaxV) agent.v = MaxV;
            if (agent.a < Maxa) agent.a = -Maxa;

            if (r1 != null)
            {
                if(agent.x >r1.W ) agent.x = 0;
                if(agent.y >r1.H ) agent.y = 0;
                if (agent.x <0 ) agent.x = r1.W;
                if (agent.y <0) agent.y = r1.H;
            }
        }
    }

    public void UpdateHeal()
    {

    }

    public void UpdateFromArea() { }

    public void UpdateToArea() { }

    public void Plot(RArea r1)
    {
        var plotModel = new PlotModel { Title = "Agent Locations" };

        foreach (var agent in Items)
        {
            var scatterSeries = new ScatterSeries()
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 2,
                MarkerFill = OxyColor.FromRgb(10,10,(byte)Rnd.Next(50,220))
            };
            scatterSeries.Points.Add(new ScatterPoint(agent.x, agent.y));
            plotModel.Series.Add(scatterSeries);
        }

        var f = new GraphForm();
        var k1 = r1.W / r1.H;
        var HSize = (int)900;
        f.Width = HSize;
        f.Height = (int)(HSize / k1);

        // Create a plot view and display the plot
        //  var plotView = new OxyPlot.WindowsForms.PlotView();
        f.plotView1.Model = plotModel;

        f.Show();
    }

    public void PlotList(RArea r1, List<Agent> agents)
    {
        var plotModel = new PlotModel { Title = "Agent Locations" };

        foreach (var agent in agents)
        {
            var scatterSeries = new ScatterSeries()
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 2,
                MarkerFill = OxyColor.FromRgb(10, 10, (byte)Rnd.Next(50, 220))
            };
            scatterSeries.Points.Add(new ScatterPoint(agent.x, agent.y));
            plotModel.Series.Add(scatterSeries);
        }

        var f = new GraphForm();
        var k1 = r1.W / r1.H;
        var HSize = (int)900;
        f.Width = HSize;
        f.Height = (int)(HSize / k1);

        // Create a plot view and display the plot
        //  var plotView = new OxyPlot.WindowsForms.PlotView();
        f.plotView1.Model = plotModel;

        f.Show();
    }

    #region Util

    public List<Agent> ListByState(CovidState state)
    {
        var res = Items.Where(a=>a.State == state).ToList();
        return res;
    }

    public List<Agent> SuspectedAgents() => ListByState(CovidState.Suspected);
    public List<Agent> ExposedAgents() => ListByState(CovidState.Exposed);
    public List<Agent> InfectedAgents() => ListByState(CovidState.Infected);
    public List<Agent> DeadAgents() => ListByState(CovidState.Dead);
    public List<Agent> RecoveredAgents() => ListByState(CovidState.Recovered);
    public List<Agent> VaccinatedAgents() => ListByState(CovidState.Vaccinated);

    public int CountOfSuspected => SuspectedAgents().Count;
    public int CountOfExposed => ExposedAgents().Count;
    public int CountOfInfected => InfectedAgents().Count;
    public int CountOfDead => DeadAgents().Count;
    public int CountOfRecovered => RecoveredAgents().Count;
    public int CountOfVaccinated => VaccinatedAgents().Count;

    public List<Agent> SuspectedNear(Agent agent, double dist)
    {
        var res = Items.Where(item => item.State == CovidState.Suspected).Where(item => agent.IsNearQ(item, dist)).ToList();
        return res;
    }


    #endregion

}