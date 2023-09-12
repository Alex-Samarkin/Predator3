using PLibrary1;
using Region = PLibrary1.Region;

namespace GUIApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            propertyGrid1.SelectedObject = About;
        }

        public PLibrary1.About About { get; set; } = new PLibrary1.About();
        private void button1_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = About;

        }

        public RArea r1 { get; set; } = new RArea();
        private void button2_Click(object sender, EventArgs e)
        {
            r1.ResetField();
            propertyGrid1.SelectedObject = r1;
            r1.RandomFill(500);
            r1.Laplacian();
            r1.Plot();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                r1.Run(false);
                if (i % 100 == 0) { r1.Run(); }
            }
        }

        public Koeff Koeff { get; set; } = new Koeff();
        private void button4_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = Koeff;
        }

        public Agent Agent { get; set; } = new Agent();
        private void button5_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = Agent;

            AgentGenerator generator = new AgentGenerator();
            //generator.Seed = generator.Rnd.Next();
            //generator.ResetRng();

            generator.BuildNewAgent();

            Agent = generator.NewAgent;
        }

        public Virus Virus { get; set; } = new Virus();
        private void button6_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = Virus;
        }

        public AgentGenerator Generator { get; set; } = new AgentGenerator();
        private void button7_Click(object sender, EventArgs e)
        {
            Generator.Xmax = r1.W;
            Generator.Ymax = r1.H;
            propertyGrid1.SelectedObject = Generator;
        }

        public Agents Agents { get; set; } = new Agents();
        private void button8_Click(object sender, EventArgs e)
        {
            Agents.FillWithGenerator(7000, Generator, true);
            Agents.InfectNPeoples(100);
            propertyGrid1.SelectedObject = Agents;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Agents.Plot(r1);
            Agents.Run(r1);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var infected = Agents.InfectedAgents();
            Agents.PlotList(r1, infected);
            propertyGrid1.SelectedObject = infected;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var suspected = Agents.SuspectedAgents();
            Agents.PlotList(r1, suspected);
            propertyGrid1.SelectedObject = suspected;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var exposed = Agents.ExposedAgents();
            Agents.PlotList(r1, exposed);
            propertyGrid1.SelectedObject = exposed;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var recovered = Agents.RecoveredAgents();
            Agents.PlotList(r1, recovered);
            propertyGrid1.SelectedObject = recovered;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var dead = Agents.DeadAgents();
            Agents.PlotList(r1, dead);
            propertyGrid1.SelectedObject = dead;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var vaccinated = Agents.VaccinatedAgents();
            Agents.PlotList(r1, vaccinated);
            propertyGrid1.SelectedObject = vaccinated;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Agents.Run(r1);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var n = trackBar1.Value;
            for (int i = 0; i < n; i++)
            {
                Agents.Run(r1);
            }
        }

        public Area Area { get; set; } = new Area();
        private void button18_Click(object sender, EventArgs e)
        {
            Area.Init(42, 500, 500, 0.1, 42000, 400, 0);
            propertyGrid1.SelectedObject = Area;
            Area.RArea.Plot();
            Area.Agents.Plot(Area.RArea);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Area.Run1();
            Area.RArea.Plot();
            Area.Agents.Plot(Area.RArea);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Area.RunN();
            Area.RArea.Plot();
            Area.Agents.Plot(Area.RArea);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Region.Init();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Region.Run1();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Region.RunN();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            // Region.RunAll();
            Region.RunAsync();
        }

        public Region Region { get; set; } = new Region();

    }
}