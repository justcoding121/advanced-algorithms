namespace Advanced.Algorithms.Geometry
{
    public class Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        public double X { get; set; }
        public double Y { get; set; }

        public override bool Equals(object obj)
        {
            var tgt = obj as Point;
            return (tgt.X == X) && (tgt.Y == Y);
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}
