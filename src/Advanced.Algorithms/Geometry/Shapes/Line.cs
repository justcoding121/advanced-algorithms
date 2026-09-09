using System;

namespace Advanced.Algorithms.Geometry;

/// <summary>
///     Line object.
/// </summary>
public class Line
{
    private readonly Lazy<double> slope;
    private readonly double tolerance;

    private Line()
    {
        slope = new Lazy<double>(() => CalcSlope());
    }

    internal Line(Point start, Point end, double tolerance)
        : this()
    {
        this.tolerance = tolerance;

        if (start.X.IsLessThan(end.X, tolerance)
            || (start.X.IsEqual(end.X, tolerance) && start.Y.IsLessThan(end.Y, tolerance)))
        {
            Left = start;
            Right = end;
        }
        else
        {
            Left = end;
            Right = start;
        }
    }

    public Line(Point start, Point end, int precision = 5)
        : this(start, end, Math.Round(Math.Pow(0.1, precision), precision))
    {
    }

    public Point Left { get; }
    public Point Right { get; }

    public bool IsVertical => Left.X.IsEqual(Right.X, tolerance);
    public bool IsHorizontal => Left.Y.IsEqual(Right.Y, tolerance);

    public double Slope => slope.Value;

    private double CalcSlope()
    {
        Point left = Left, right = Right;

        //vertical line has infinite slope
        if (left.X.IsEqual(right.X, tolerance)) return double.MaxValue;

        return (right.Y - left.Y) / (right.X - left.X);
    }

    public Line Clone()
    {
        return new Line(Left.Clone(), Right.Clone());
    }
}
