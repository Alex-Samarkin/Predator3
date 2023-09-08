// Predator3
// PLibrary1
// RArea.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 1:05 08 09 2023

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Random;

using System.Drawing;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using MathNet.Numerics.Statistics;

namespace PLibrary1;

public class RArea
{
    public Pt Start { get; set; } = new Pt() { X = 0, Y = 0, Value = 0 };
    public double W { get; set; } = 200;
    public double H { get; set; } = 150;
    public double Angle = 0;

    public Pt End { get => new Pt(){ X = Start.X+
                        W*Math.Cos(Angle)-H*Math.Sin(Angle),
                                     Y = Start.Y+ 
                        W*Math.Sin(Angle)+H*Math.Cos(Angle)}; }

    public double Step { get; set; } = 1;

    public Matrix<double> Field { get; set; }

    public RArea()
    {
        Field = Matrix<double>.Build.Dense((int)H, (int)W);
    }

    public void ResetField()
    {
        Field = Matrix<double>.Build.Dense((int)H, (int)W);
    }

    public double Value(double X, double Y)
    {
        // as torus
        if (X < 0) X = W - 1;
        if (Y < 0) Y = H - 1;
        if (X > W) X = 0;
        if (Y > H) Y = 0;
        return Field[(int)X, (int)Y];
    }

    public Matrix<double> Laplasian()
    {
        // Create a Laplacian kernel
        Matrix<double> laplacianKernel = DenseMatrix.OfArray(new double[,]
        {
            { -1, -1, -1 },
            { -1,  8, -1 },
            { -1, -1, -1 }
        });
        // Compute the Laplacian using convolution
        // Matrix<double> laplacianResult = Field.Convolve(laplacianKernel);
        return laplacianKernel;
    }

    public void RandomFill(int N)
    {
       // Create a random number generator
        var rng = new MersenneTwister();

        int rows = (int) H; int cols = (int) W;

        for (int i = 0; i < N; i++)
        {
            var newValue = rng.NextDouble();
            Field[ rng.Next(rows), rng.Next(cols)] = newValue;
        }
    }

    public void Plot()
    {
        // Create a PlotModel
        var plotModel = new PlotModel();

        // Create a HeatMapSeries
        var heatMapSeries = new HeatMapSeries
        {
            X0 = 0,
            X1 = (int)W,
            Y0 = 0,
            Y1 = (int)H,
            Interpolate = true // Set to true for smoother interpolation
        };
        // Add the data points from the matrix to the HeatMapSeries
        // Convert the matrix data into a 2D array for the HeatMapSeries
        double[,] dataArray = new double[(int)H, (int)W];

        for (int i = 0; i < (int)H; i++)
        {
            for (int j = 0; j < (int)W; j++)
            {
                dataArray[i, j] = Field[i, j];
            }
        }

        heatMapSeries.Data = dataArray;
        
        // Add the HeatMapSeries to the PlotModel
        plotModel.Series.Add(heatMapSeries);

        // Create a color axis
        var colorAxis = new LinearColorAxis
        {
            Position = AxisPosition.Right,
            Palette = OxyPalettes.BlueWhiteRed(100)
        };

        // Add the color axis to the PlotModel
        plotModel.Axes.Add(colorAxis);

        

        var f = new GraphForm();
        f.Width = (int)W*3+50;
        f.Height = (int)H*3+50;
        
        // Create a plot view and display the plot
        //  var plotView = new OxyPlot.WindowsForms.PlotView();
        f.plotView1.Model = plotModel;

        f.Show();
    }

    public void Laplacian()
    {
       // Create a Laplacian kernel
        Matrix<double> laplacianKernel = DenseMatrix.OfArray(new double[,]
        {
            { -1, -1, -1 },
            { -1,  8, -1 },
            { -1, -1, -1 }
        });

        var rows = (int) H; var cols = (int) W;

        // Create a result matrix to store the Laplacian values
        Matrix<double> laplacianResult = DenseMatrix.Build.Dense(rows, cols);

        // Compute the Laplacian by iterating through the elements
        for (int i = 1; i < rows - 1; i++)
        {
            for (int j = 1; j < cols - 1; j++)
            {
                double sum = 0;

                // Apply the Laplacian kernel to the neighborhood of each element
                for (int k = -1; k <= 1; k++)
                {
                    for (int l = -1; l <= 1; l++)
                    {
                        sum += Field[i + k, j + l] * laplacianKernel[k + 1, l + 1];
                    }
                }

                laplacianResult[i, j] = sum/8.0;
            }
        }

        // Print the Laplacian result
        // Console.WriteLine("Laplacian Result:");
        // Console.WriteLine(laplacianResult);
        var min = Statistics.Minimum(laplacianResult.Enumerate());
        var max = Statistics.Maximum(laplacianResult.Enumerate());
        Field = (laplacianResult.NormalizeRows(2)-min)/(max-min);
    }

    public void Run(bool Show = true)
    {
        Laplacian();
        if (Show) Plot();
    }


}
