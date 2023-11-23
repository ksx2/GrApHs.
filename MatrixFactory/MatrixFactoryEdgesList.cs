
namespace GraphBase.GraphMatrixFactory
{
    public class Matrix_Factory_Edge_List : Matrix_Factory
    {
        public Matrix_Factory_Edge_List(string file_path) : base(file_path) { }

        public override List<List<int>> Get_Matrix()
        {
            using (var reader = new StreamReader(file_path))
            {
                var edges = reader.ReadToEnd().Split('\n').Where(s => s.Any()).Select(
                          s => s.Split(' ').Where(s => s.Any()).Select(s => Convert.ToInt32(s)).ToArray());
                int max = edges.Select(e => e.Take(2).Max()).Max();

                var matrix = Create_Empty_Matrix(max);

                bool haveWeight = false;
                if (edges.First().Length > 2) haveWeight = true;

                foreach (var a in edges)
                {
                    matrix[a[0] - 1][a[1] - 1] = haveWeight ? a[2] : 1;
                }

                return matrix;
            }
        }
    }
}

