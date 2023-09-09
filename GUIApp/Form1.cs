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
        }

        public Virus Virus { get; set; } = new Virus();
        private void button6_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = Virus;
        }
    }
}