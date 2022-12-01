using System;
using System.Globalization;

class Program
{
    static void Main(String[] args)
    {
        Console.WriteLine("Insertion Sort");
        int[] a = new int[] { 4, 2, 7, 5, -1, 55, 20, 10 };
        //for insertion sort
        InsertionSort(a);
        for (int i = 0; i < a.Length; i++)
        {
            Console.Write($"{a[i]} ");
        }

        Console.WriteLine("");

        Console.WriteLine("Merge Sort");

        //for Merge Sort
        MergeSort(a, 0, a.Length - 1);
        for (int i = 0; i < a.Length; i++)
        {
            Console.Write($"{a[i]} ");
        }

        Console.WriteLine("");

        //for Kruskal
        int verticesCount = 4;
        int edgesCount = 5;
        Graph graph = CreateGraph(verticesCount, edgesCount);

        // Edge 0-1
        graph.edge[0].Source = 0;
        graph.edge[0].Destination = 1;
        graph.edge[0].Weight = 10;

        // Edge 0-2
        graph.edge[1].Source = 0;
        graph.edge[1].Destination = 2;
        graph.edge[1].Weight = 6;

        // Edge 0-3
        graph.edge[2].Source = 0;
        graph.edge[2].Destination = 3;
        graph.edge[2].Weight = 5;

        // Edge 1-3
        graph.edge[3].Source = 1;
        graph.edge[3].Destination = 3;
        graph.edge[3].Weight = 15;

        // Edge 2-3
        graph.edge[4].Source = 2;
        graph.edge[4].Destination = 3;
        graph.edge[4].Weight = 4;

        Kruskal(graph);
        Console.Read();

    }
    private static void InsertionSort(int[] a)
    {
        int cnt = a.Length;
        int key;
        for (int i = 1; i < cnt; i++)
        {
            key = a[i];
            int j = i - 1;
            while (j >= 0 && a[j] > key)
            {
                a[j + 1] = a[j];
                j = j - 1;
            }
            a[j + 1] = key;
        }
    }
    private static void MergeSort(int[] a, int l, int r)
    {
        if (l < r)
        {
            int m = l + (r - l) / 2 + 1;
            MergeSort(a, l, m - 1);
            MergeSort(a, m, r);
            Merge(a, l, m, r);
        }
    }
    private static void Merge(int[] a, int l, int m, int r)
    {
        int i, j, k;
        int lal = m - l, ral = r - m + 1;
        int[] left = new int[lal];
        int[] right = new int[ral];
        for (i = 0; i < lal; i++)
        {
            left[i] = a[l + i];
        }
        for (i = 0; i < ral; i++)
        {
            right[i] = a[m + i];
        }
        i = 0; j = 0; k = l;
        while (i < lal && j < ral)
        {
            if (left[i] <= right[j])
            {
                a[k++] = left[i++];
            }
            else
            {
                a[k++] = right[j++];
            }
        }
        if (i == lal)
        {
            for (int ii = j; ii < ral; ii++)
            {
                a[k++] = right[ii];
            }
        }
        if (j == ral)
        {
            for (int ii = i; ii < lal; i++)
            {
                a[k++] = left[ii];
            }
        }

    }
    public struct Edge
    {
        public int Source;
        public int Destination;
        public int Weight;
    }

    public struct Graph
    {
        public int VerticesCount;
        public int EdgesCount;
        public Edge[] edge;
    }

    public struct Subset
    {
        public int Parent;
        public int Rank;
    }

    public static Graph CreateGraph(int verticesCount, int edgesCoun)
    {
        Graph graph = new Graph();
        graph.VerticesCount = verticesCount;
        graph.EdgesCount = edgesCoun;
        graph.edge = new Edge[graph.EdgesCount];

        return graph;
    }
    private static int Find(Subset[] subsets, int i)
    {
        if (subsets[i].Parent != i)
            subsets[i].Parent = Find(subsets, subsets[i].Parent);

        return subsets[i].Parent;
    }

    private static void Union(Subset[] subsets, int x, int y)
    {
        int xroot = Find(subsets, x);
        int yroot = Find(subsets, y);

        if (subsets[xroot].Rank < subsets[yroot].Rank)
            subsets[xroot].Parent = yroot;
        else if (subsets[xroot].Rank > subsets[yroot].Rank)
            subsets[yroot].Parent = xroot;
        else
        {
            subsets[yroot].Parent = xroot;
            ++subsets[xroot].Rank;
        }
    }

    private static void Print(Edge[] result, int e)
    {
        for (int i = 0; i < e; ++i)
            Console.WriteLine("{0} -- {1} == {2}", result[i].Source, result[i].Destination, result[i].Weight);
    }
    public static void Kruskal(Graph graph)
    {
        int verticesCount = graph.VerticesCount;
        Edge[] result = new Edge[verticesCount];
        int i = 0;
        int e = 0;

        Array.Sort(graph.edge, delegate (Edge a, Edge b)
        {
            return a.Weight.CompareTo(b.Weight);
        });

        Subset[] subsets = new Subset[verticesCount];

        for (int v = 0; v < verticesCount; ++v)
        {
            subsets[v].Parent = v;
            subsets[v].Rank = 0;
        }

        while (e < verticesCount - 1)
        {
            Edge nextEdge = graph.edge[i++];
            int x = Find(subsets, nextEdge.Source);
            int y = Find(subsets, nextEdge.Destination);

            if (x != y)
            {
                result[e++] = nextEdge;
                Union(subsets, x, y);
            }
        }

        Print(result, e);
    }

}
