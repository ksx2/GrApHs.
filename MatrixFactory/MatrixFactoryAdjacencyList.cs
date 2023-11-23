
namespace GraphBase.GraphMatrixFactory
{
    public class Matrix_Factory_Adjacency_List : Matrix_Factory
    {
        public Matrix_Factory_Adjacency_List(string file_path) : base(file_path) { }

        public override List<List<int>> Get_Matrix()
        {
            using (var reader = new StreamReader(file_path))
            {
                var lines = reader.ReadToEnd().Split('\n').Where(s => s.Any()).ToArray();
                var matrix = Create_Empty_Matrix(lines.Length);

                for (int i = 0; i < lines.Length; i++)
                {
                    var values = lines[i].Split(' ').Where(s => s.Any()).Select(s => Convert.ToInt32(s)).ToList();
                    {
                        for (int j = 0; j < values.Count; j++)
                        {
                            matrix[i][values[j] - 1] = 1;
                        }
                    }
                }
                return matrix;
            }

        }
    }
}
