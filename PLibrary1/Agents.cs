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

    public double MaxDa { get; set; } = 0.2;
    public double MaxDAngle { get; set; } = 30.0 * Math.PI / 180.0;
    public double Maxa { get; set; } = 2;
    public double MaxV { get; set; } = 10;

    public double DistanceToInfection { get; set; } = 4;

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
            var agent = generator.BuildNewAgent();
            agent.Index = i;
            Items.Add(agent);
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
    public void VaccinateNPeoples(int N)
    {
        var unvaccinated = Items.Where(a => (a.State != CovidState.Infected));
        var Indexes = new SortedSet<int>();

        while (Indexes.Count < N)
        {
            Indexes.Add(Rnd.Next(Items.Count));
        }

        foreach (int index in Indexes)
        {
            Items[index].SetState(CovidState.Vaccinated);
        }
    }
    public void Clear() { Items.Clear(); }

    public void Run(RArea r1, bool Show = false)
    {
        IncTime();
        Move();
        UpdateVA(r1);
        UpdateHeal();
        if (Show)
        {
            Plot(r1);
        }
    }
    //00 Увеличить время жизни и время состояния агентов на 1 день
    public void IncTime(int Step = 1)
    {
        foreach (Agent agent in Items)
        {
            agent.TimeFromStart += Step;
            agent.TimeOfState += Step;
        }
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
        /// Процесс заражения
        /// 01 Ищем всех зараженных
        var infected = this.InfectedAgents();
        /// 02 Для каждого зараженного
        foreach (Agent agent in infected)
        {
            /// 03 Ищем всех незараженных (со статусом Suspected) на заданном расстоянии
            var suspected = this.SuspectedNear(agent,DistanceToInfection);
            /// 04 Для каждого незараженного
            foreach (Agent agent1 in suspected)
            {
                /// 05 если за это время его статус не изменился (например, в другом потоке)
                if (agent1.State == CovidState.Suspected)
                {
                    /// 06 Пробуем заразить
                    /// Для этого генерируем случайное число от 0 до 1
                    /// Сравниваем с локальным для человека вирусом (там заданы все поправки)
                    /// Например, вирус имеет вероятность заражения 0,3
                    /// Человек имеет поправочный коэффициент 0,8
                    /// Вероятность заражения 0,3*0,8=0,24
                    /// Генератор выдал число 0,6 - заражения не происходит
                    /// Генератор выдал 0,15 - происходит заражение,
                    /// агент получает статус Exposed
                    agent1.TryToInfect(Rnd.NextDouble());
                }
            }
        }
        /// Процессы перехода

        /// Infected - Dead or Recover
        /// 01 Для каждого зараженного, у которого истекло время болезни
        var infected1 = infected.Where(a => a.TimeOfState >= a.Virus.InfectedDuration);//.ToList();
        foreach (Agent agent in infected1)
        {
            /// 02 Каждый заболевший может получить статус как выздоровевшего, так и умершего
            /// у выздоровевшего отмеряется время до окончания иммунитета
            agent.TryToRecover(Rnd.NextDouble());
        }

        /// Exposed - Infected
        var exposed = ExposedAgents().Where(a => a.TimeOfState > a.Virus.ExposedDuration);//.ToList();
        foreach (Agent agent in exposed)
        {
            agent.SetState(CovidState.Infected);
        }

        /// Recover - Suspected
        var recover = RecoveredAgents().Where(a => a.TimeOfState > a.Virus.ImmunityDuration);//.ToList();
        foreach (Agent agent in exposed)
        {
            agent.SetState(CovidState.Suspected);
        }

        /// Vaccinated - Suspected
        var vaccinated = VaccinatedAgents().Where(a => a.TimeOfState > a.Virus.ImmunityDuration);//.ToList();
        foreach (Agent agent in exposed)
        {
            agent.SetState(CovidState.Suspected);
        }
    }

    public void UpdateFromArea() { }

    public void UpdateToArea() { }

    public void Plot(RArea r1)
    {
        var plotModel = new PlotModel { Title = "Agent Locations" };

        var scatterSeries = new ScatterSeries()
        {
            MarkerType = MarkerType.Circle,
            MarkerSize = 2,
            MarkerFill = OxyColor.FromRgb(10, 10, (byte)Rnd.Next(120, 220))
        };

        foreach (var agent in Items)
        {
            
            scatterSeries.Points.Add(new ScatterPoint(agent.x, agent.y));
            
        }
        
        plotModel.Series.Add(scatterSeries);
        
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
    public void Plot2(RArea r1)
    {
        var plotModel = new PlotModel { Title = "Agent Locations" };

        var SscatterSeries = new ScatterSeries()
        {
            MarkerType = MarkerType.Circle,
            MarkerSize = 2,
            MarkerFill = OxyColor.FromRgb(10, 10, 180)
        };
        var EscatterSeries = new ScatterSeries()
        {
            MarkerType = MarkerType.Circle,
            MarkerSize = 2,
            MarkerFill = OxyColor.FromRgb(110, 10, 10)
        };
        var IscatterSeries = new ScatterSeries()
        {
            MarkerType = MarkerType.Triangle,
            MarkerSize = 2,
            MarkerFill = OxyColor.FromRgb(220, 10, 10)
        };
        var RscatterSeries = new ScatterSeries()
        {
            MarkerType = MarkerType.Circle,
            MarkerSize = 2,
            MarkerFill = OxyColor.FromRgb(10, 180, 10)
        };
        var DscatterSeries = new ScatterSeries()
        {
            MarkerType = MarkerType.Cross,
            MarkerSize = 2,
            MarkerFill = OxyColor.FromRgb(10, 10, 10)
        };

        foreach (var agent in Items)
        {
            if (agent.State == CovidState.Suspected)
                SscatterSeries.Points.Add(new ScatterPoint(agent.x, agent.y));
            if (agent.State == CovidState.Exposed)
                EscatterSeries.Points.Add(new ScatterPoint(agent.x, agent.y));
            if (agent.State == CovidState.Infected)
                IscatterSeries.Points.Add(new ScatterPoint(agent.x, agent.y));
            if (agent.State == CovidState.Recovered)
                RscatterSeries.Points.Add(new ScatterPoint(agent.x, agent.y));
            if (agent.State == CovidState.Dead)
                DscatterSeries.Points.Add(new ScatterPoint(agent.x, agent.y));
        }

        plotModel.Series.Add(SscatterSeries);
        plotModel.Series.Add(EscatterSeries);
        plotModel.Series.Add(IscatterSeries);
        plotModel.Series.Add(RscatterSeries);
        plotModel.Series.Add(DscatterSeries);

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