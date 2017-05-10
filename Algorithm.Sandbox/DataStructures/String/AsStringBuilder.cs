using System.Collections.Generic;

namespace Algorithm.Sandbox.DataStructures
{
    public class AsStringBuilder
    {
        private List<string> builder = new List<string>();

        public void Append(string s)
        {
            builder.Add(s);
        }
        //TODO is there a way to avoid string.Concat (perhaps implement one ourselfves?)
        public override string ToString()
        {
            return string.Concat(builder.ToArray());
        }
    }
}
