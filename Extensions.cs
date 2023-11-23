namespace GraphBase.Extentions
{
    public static class MatrixExtentions
    {
        public static IEnumerable<int> AdjacencyList(this List<List<int>> matrix, int v)
        {
            if (v < 0 || v > matrix.Count) throw new Exception($"Обращение к несуществующей вершине!Обращение к {v}");

            for (int i = 0; i < matrix.Count; i++)
            {
                if (matrix[v][i] != 0) yield return i;
            }
        }
    }
}
