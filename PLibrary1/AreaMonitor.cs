using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace PLibrary1
{
    public partial class AreaMonitor : Form
    {
        public AreaMonitor()
        {
            InitializeComponent();
        }

        public void PlotArea(Area area, bool PlotAgents = true)
        {
            S = area.CountOfSuspected;
            E = area.CountOfExposed;
            I = area.CountOfInfected;
            R = area.CountOfInfected;
            D = area.CountOfDead;
            V = area.CountOfVaccinated;

            PlotS();
            PlotE();
            PlotI();
            PlotR();
            PlotD();
            PlotV();

            Plot_All(plotView7);

            if (PlotAgents)
            {
                PlotXY(area.Agents,plotView8);
            }

        }
        
        private List<int> S { get; set; } = new List<int>();
        private List<int> E { get; set; } = new List<int>();
        private List<int> I { get; set; } = new List<int>();
        private List<int> R { get; set; } = new List<int>();
        private List<int> D { get; set; } = new List<int>();
        private List<int> V { get; set; } = new List<int>();

        Agents A = new Agents();

        public void plotLine(PlotView pv, List<int> data, string title, string label, OxyColor color)
        {
            // Create a PlotModel
            var plotModel = new PlotModel { Title = title };

            // Create LineSeries for each line
            var lineSeries = new LineSeries { Title = label, Color = color };


            var i = 0;
            foreach (int value in data)
            {
                lineSeries.Points.Add(new DataPoint(i,value));
                i++;
            }
            plotModel.Series.Add(lineSeries);

            pv.Model = plotModel;
        }

        private LineSeries CreateLine(List<int> data, string label, OxyColor color)
        {
            var lineSeries = new LineSeries { Title = label, Color = color };
            var i = 0;
            foreach (int i1 in data)
            {
                lineSeries.Points.Add(new DataPoint(i,i1));
                i++;
            }

            return lineSeries;
        }

        public void PlotS()=> plotLine(plotView1,S,"Suspected","S",OxyColors.DarkBlue);
        public void PlotE() => plotLine(plotView2, E, "Expected", "E", OxyColors.Violet);
        public void PlotI() => plotLine(plotView3, I, "Infected", "I", OxyColors.Orange);
        public void PlotR() => plotLine(plotView4, R, "Recover", "R", OxyColors.Green);
        public void PlotD() => plotLine(plotView5, D, "Dead", "D", OxyColors.Black);
        public void PlotV() => plotLine(plotView6, V, "Vaccinated", "V", OxyColors.DarkTurquoise);

        public void Plot_All(PlotView pv)
        {
            // Create a PlotModel
            var plotModel = new PlotModel { Title = "SEIRDV" };

            plotModel.Series.Add(CreateLine(S, "S", OxyColors.DarkBlue));
            plotModel.Series.Add(CreateLine(E, "E", OxyColors.Violet));
            plotModel.Series.Add(CreateLine(I, "I", OxyColors.Orange));
            plotModel.Series.Add(CreateLine(R, "R", OxyColors.Green));
            plotModel.Series.Add(CreateLine(D, "D", OxyColors.Black));
            plotModel.Series.Add(CreateLine(V, "V", OxyColors.DarkTurquoise));

            pv.Model = plotModel;

        }

        public void PlotXY(Agents agents, PlotView pv)
        {

        }

    }
}
