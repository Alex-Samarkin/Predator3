// Predator3
// PLibrary1
// Pt.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 1:05 08 09 2023

using System.Drawing.Drawing2D;
using System.Security.Cryptography.Xml;

namespace PLibrary1;

public class Pt
{
    public double[] Coord { get; set; } = new double[2];
    public double Value { get; set; } = 0;

    public double X { get => Coord[0]; set => Coord[0] = value; }
    public double Y { get => Coord[1]; set => Coord[1] = value; }

    public double Z { get => Value; set => Value = value; }

    public double Dx(Pt other) => Coord[0] - other.X;
    public double Dy(Pt other) => Coord[1] - other.Y;
    public double Dist2 (Pt other) => Dx(other)*Dx(other)+Dy(other)*Dy(other);
    public double Dist(Pt other) => Math.Sqrt(Dist2(other));
    public double Angle(Pt other) => Math.Atan2(Dy(other), Dx(other));
}