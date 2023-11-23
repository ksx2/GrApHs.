using GraphBase.GraphMatrixFactory;
using System.Collections.Generic;

namespace GraphBase.Objects
{

    public class Graph
    {
        private readonly List<List<int>> matrix_of_adjacency;//матрица смежности
        private List<List<int>> transponse_matrix_of_adjacency;//транспонированная матрица смежности

        public Graph(Matrix_Factory factory)//конструктор через фабрику
        {
            matrix_of_adjacency = factory.Get_Matrix();
            transponse_matrix_of_adjacency = factory.Get_Matrix();
        }

        public Graph(List<List<int>> matrix)//конструктор по умолчанию
        {
            matrix_of_adjacency = matrix;
        }

        public int Vertex_Count() => matrix_of_adjacency.Count;
        public int Weight(int vi, int vj)//вычисление веса ребра
        {
            if (Is_edge(vi, vj))
                return matrix_of_adjacency[vi - 1][vj - 1];
            else throw new Exception($"Ребро {vi}-{vj} не существует!");
        }

        public void Print()
        {
            for (int i = 0; i < matrix_of_adjacency.Count; i++)
            {
                for (int j = 0; j < matrix_of_adjacency.Count; j++)
                {
                    Console.Write($"{matrix_of_adjacency[i][j]} ");
                }
                Console.WriteLine();
            }
        }
        public bool Is_edge(int vi, int vj)//проверка наличия ребра
        {
            if (matrix_of_adjacency[vi - 1][vj - 1] == 0
                || vi > matrix_of_adjacency.Count
                || vj > matrix_of_adjacency.Count
                || vi < 0
                || vj < 0)
            {
                return false;
            }
            return true;
        }
        public List<List<int>> Adjacency_matrix() => matrix_of_adjacency.Select(l => l.ToList()).ToList();//вычисление матрицы смежности

        public List<(int, int)> AdjacencyList(int v, List<List<int>> matrix_of_adjacency)
        {
            List<(int, int)> list = new List<(int, int)>();
            if (v < 0 || v > matrix_of_adjacency.Count) throw new Exception($"Обращение к несуществующей вершине!Обращение к {v}");

            for (int i = 0; i < matrix_of_adjacency.Count; i++)
            {
                if (matrix_of_adjacency[v][i] != 0)
                {
                    list.Add((v, i));
                }
            }
            return list.ToList();
        }
        public IEnumerable<(int, int)> List_of_edges()//вычисление всех ребер графа
        {
            var edges = new HashSet<(int, int)>();
            for (int i = 0; i < matrix_of_adjacency.Count; i++)
            {
                for (int j = 0; j < matrix_of_adjacency[i].Count; j++)
                {
                    if (matrix_of_adjacency[i][j] != 0)
                    {
                        edges.Add((i, j));
                    }
                }
            }
            return edges;
        }
        public IEnumerable<(int, int)> List_of_edges(int v)//вычисление ребер инцидентных данной
        {
            if (v > matrix_of_adjacency.Count || v < 0)
                throw new Exception("ОБращение к несуществующей вершине!!!");
            for (int i = 0; i < matrix_of_adjacency.Count; i++)
            {
                if (matrix_of_adjacency[v][i] > 0) yield return (v, i);
            }
        }
        public bool IsDirected()
        {
            for (int i = 0; i < matrix_of_adjacency.Count; i++)
            {
                for (int j = i; j < matrix_of_adjacency.Count; j++)
                {
                    if (matrix_of_adjacency[i][j] != matrix_of_adjacency[j][i]) return true;
                }
            }

            return false;
        }

        public List<List<int>> GetCorrelatedMatrix()
        {
            var result = Adjacency_matrix();

            if (!IsDirected()) return result;

            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < result.Count; j++)
                {
                    if (Is_edge(i, j))
                        result[j][i] = result[i][j];
                }
            }
            return result;
        }
        public List<List<int>> Transpose()
        {
            transponse_matrix_of_adjacency = matrix_of_adjacency.Select(l => l.ToList()).ToList();

            for (int i = 0; i < transponse_matrix_of_adjacency.Count; i++)
            {
                for (int j = i; j < transponse_matrix_of_adjacency.Count; j++)
                {

                    (transponse_matrix_of_adjacency[i][j], transponse_matrix_of_adjacency[j][i]) =
                    (transponse_matrix_of_adjacency[j][i], transponse_matrix_of_adjacency[i][j]);

                }
            }

            return transponse_matrix_of_adjacency;
        }

    }
}