namespace Advanced.Algorithms.Geometry
{
    /// <summary>
    /// Point object.
    /// </summary>
    public class Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; private set; }
        public double Y { get; private set; }

        public override string ToString()
        {
            return X.ToString("F") + " " + Y.ToString("F");
        }

        public Point Clone()
        {
            return new Point(X, Y);
        }
    }
}
