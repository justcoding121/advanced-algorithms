namespace Algorithm.Sandbox.DataStructures
{
    public class AsStringBuilder
    {
        private AsArrayList<string> builder = new AsArrayList<string>();

        public void Append(string s)
        {
            builder.AddItem(s);
        }
        //TODO is there a way to avoid string.Concat (perhaps implement one ourselfves?)
        public override string ToString()
        {
            return string.Concat(builder.ToArray());
        }
    }
}
