using GraphBase.GraphMatrixFactory;

namespace GraphBase.IO
{
    public enum AlgoKey
        {
            Dijkstra,
            Levit,
            Bellman,
            None
        };
    public class Input
    {
       
        private readonly string[] _args;
        private readonly string _header =
        "Студент: Моторкин Максим Евгеньевич\nГруппа: М3О-309Б-21\n" +
        "Список ключей: " +
        "-e \"edges_list_file_path\" - файл со списком ребер" +
        "\r\n-m \"adjacency_matrix_file_path\" - файл с матрицей смежности" +
        "\r\n-l \"adjacency_list_file_path\" - файл со списком смежности" +
        "\r\n-o \"output_file_path\" - файл для выходных данных";
        private readonly Dictionary<string, Func<string, Matrix_Factory>> Factories = new()
        {
            ["-e"] = (path) => new Matrix_Factory_Edge_List(path),
            ["-m"] = (path) => new Matrix_Factory_Adjacency_Matrix(path),
            ["-l"] = (path) => new Matrix_Factory_Adjacency_List(path)
        };
        public Input(string[] args)
        {
            if (args.Where(f => Factories.ContainsKey(f)).Count() > 1)
            {
                throw new ArgumentException("Использованно более одного флага ввода!");
            }

            _args = args;
        }
        public Matrix_Factory GetFactory()
        {
            Matrix_Factory? factory = null;

            for (int i = 0; i < _args.Length; i++)
            {
                if (Factories.ContainsKey(_args[i]))
                {
                    factory = Factories[_args[i]](_args[i + 1]);
                }
            }

            if (factory == null) { throw new Exception("Укажите путь к файлу!"); }

            return factory;
        }

        public bool IsArgsContainsHeader()
        {
            if (_args.FirstOrDefault(s => s == "-h") != null)
            {
                Console.WriteLine(_header);
                return true;
            }

            return false;
        }
        public AlgoKey IsArgsContainsAlgoKey()
        {
            if (_args.FirstOrDefault(s => s == "-d") != null)
            {
                return AlgoKey.Dijkstra;
            }
            if (_args.FirstOrDefault(s => s == "-b") != null)
            {
                return AlgoKey.Bellman;
            }
            if (_args.FirstOrDefault(s => s == "-lev") != null)
            {
                return AlgoKey.Levit;
            }
            Console.WriteLine("Algorithm key is undefined.");
            return AlgoKey.None;
        }
        public int IsArgsContainsVertexNum()
        {
            for (int i = 0; i < _args.Length; i++)
            {
                if (_args[i] == "-n")
                {
                    return int.Parse(_args[i + 1]);
                }

            }
            Console.WriteLine("Vertex number is undefined.");
            return int.MaxValue;
        }
    }
}
