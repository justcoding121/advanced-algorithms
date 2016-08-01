namespace Algorithm.Sandbox.DataStructures
{
    public class AsMatrix<T>
    {
        private T[,] matrix;

        public AsMatrix(int rows, int coloums)
        {
            matrix = new T[rows,coloums];
        }
    }
}
