using PLibrary1;

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
            Agents.FillWithGenerator(50000, Generator, true);
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
            foreach (Agent agent in infected)
            {
                var tmp = Agents.SuspectedNear(agent, 10);
            }
            Agents.PlotList(r1, Agents.InfectedAgents());
            propertyGrid1.SelectedObject = Agents;
        }
    }
}