
namespace GraphBase.GraphMatrixFactory
{
    public class Matrix_Factory_Adjacency_Matrix : Matrix_Factory
    {
        public Matrix_Factory_Adjacency_Matrix(string file_path) : base(file_path) { }

        public override List<List<int>> Get_Matrix()
        {
            var matrix = new List<List<int>>();
            using (var reader = new StreamReader(file_path))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    matrix.Add(line.Split(' ').Where(s => s.Any()).Select(s => Convert.ToInt32(s)).ToList());
                }
            }
            return matrix;
        }

    }
}
