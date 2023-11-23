namespace GraphBase.GraphMatrixFactory
{
    public abstract class Matrix_Factory
    {
        protected readonly string file_path;
        public Matrix_Factory(string file_path)
        {
            this.file_path = file_path;
        }

        public abstract List<List<int>> Get_Matrix();

        protected static List<List<int>> Create_Empty_Matrix(int size)
        {
            var empty_matrix = new List<List<int>>();
            for (int i = 0; i < size; i++)
            {
                var list = new List<int>();

                for (int j = 0; j < size; j++)
                {
                    list.Add(0);
                }
                empty_matrix.Add(list);
            }
            return empty_matrix;
        }
    }
}