using GraphBase.IO;
using GraphBase.Objects;
using GraphBase.Extentions;

namespace GraphT3.BridgesAndHinges
{
    public class BridgesAndHinges
    {

        private readonly List<List<int>> matrix;
        private readonly List<int> hinges;
        private readonly List<(int v, int u)> bridges;
        public BridgesAndHinges(Graph graph)
        {
            matrix = graph.GetCorrelatedMatrix();
            FindBridgesAndHinges(out bridges, out hinges);
        }
        public IEnumerable<(int v, int u)> GetBridges() => bridges.ToList();

        public IEnumerable<int> GetHinges() => hinges.ToList();

        private void FindBridgesAndHinges(out List<(int, int)> bridges, out List<int> hinges)
        {
            var components = new DfsComponents(
                new int[matrix.Count],
                new int[matrix.Count],
                new bool[matrix.Count]
            );

            bridges = new List<(int, int)>();
            hinges = new List<int>();
            var AllVertexes = new List<int>();
            for (int i = 0; i < matrix.Count; i++) //добавляем все вершины
            {
                AllVertexes.Add(i);
            }

            while (AllVertexes.Any())//проход по всем вершинам
            {
                var oneComponent = BridgesDFS(AllVertexes.First(), bridges, hinges, components);

                foreach (var v in oneComponent)//удаление вершины,через который прошел dfs 
                {
                    AllVertexes.Remove(v);
                }
            }
        }
        private IEnumerable<int> BridgesDFS(int v, List<(int, int)> bridges, List<int> hinges, DfsComponents components, int p = -1)
        {
            var vertexes = new List<int>() { v };//вершины
            components.Used[v] = true;//текущая вершина посещена
            components.Tin[v] = components.Tup[v] = components.Timer++;//увеличиваем время
            int count = 0;//счетчик корневых вершин

            var neighbours = matrix.AdjacencyList(v);//список смежных вершин с текущей
            if (neighbours.Where(v => components.Used[v]).Count() == matrix.Count) //
            {
                return vertexes;
            }
            foreach (int to in neighbours)//перебор всех вершин смежных с текущей
            {
                if (to == p)//игнорируем то ребро предок
                {
                    continue;
                }
                if (components.Used[to])//если вершина уже посещена - обратное ребро
                {
                    components.Tup[v] = Math.Min(components.Tup[v], components.Tin[to]);//обновляем время подъема вершины
                    //минимум из времени захода в текущую вершину и возврата в смежную с текущей
                }
                else
                {
                    vertexes.AddRange(BridgesDFS(to, bridges, hinges, components, v));//добавляем вершину
                    count++;
                    components.Tup[v] = Math.Min(components.Tup[v], components.Tup[to]);//обновляем время возврата текущей вершины
                    if (components.Tup[to] >= components.Tin[v])//если время минимального подъема больше,чем время спуска
                    {
                        if (components.Tup[to] != components.Tin[v])//если время возврата из текущей в смежную не равно времени возврата захода в текущую - мост
                        {
                            bridges.Add((v, to));
                        }
                        if (p != -1 & !hinges.Contains(v))//если не предок и не содержится в списке шарниров-шарнир
                        {
                            hinges.Add(v);
                        }
                    }
                }
            }

            if (p == -1 & count >= 2)//шарнир
            {
                hinges.Add(v);
            }
            return vertexes;
        }


        private class DfsComponents
        {
            public DfsComponents(int[] tin, int[] tup, bool[] used)
            {
                Tin = tin;
                Tup = tup;
                Used = used;
            }
            public int[] Tin { get; }//время захода в глубину
            public int[] Tup { get; }//время минимального подъема из вершины
            public bool[] Used { get; }//посещенные вершины
            public int Timer { get; set; } = 0;//время
        }
        public string ConvertToString(IEnumerable<int> nums)
             => String.Concat(nums.Select(n => $"{n + 1} "));

        public string ConvertToString(IEnumerable<(int, int)> pairs)
            => String.Concat(pairs.Select(p => $"{p.Item1 + 1} - {p.Item2 + 1} "));

    }

}
