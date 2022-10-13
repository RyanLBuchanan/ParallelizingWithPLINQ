namespace ParallelizingWithPLINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();

            int[] values = Enumerable.Range(1, 10000000)
                .Select(x => random.Next(0, 1000))
                .ToArray();

            Console.WriteLine("Min, Max and Average with LINQ to Objects using a single core");
            var linqStart = DateTime.Now;
            var linqMin = values.Min(); 
            var linqMax = values.Max();
            var linqAverage = values.Average();
            var linqEnd = DateTime.Now;

            var linqTime = linqEnd.Subtract(linqStart).TotalMilliseconds;
            DisplayResults(linqMin, linqMax, linqAverage, linqTime);

            Console.WriteLine("\nMin, Max, Average with PLINQ using multiple cores");
            var plinqStart = DateTime.Now;
            var plinqMin = values.AsParallel().Min();
            var plinqMax = values.AsParallel().Max();
            var plinqAverage = values.AsParallel().Average();
            var plinqEnd = DateTime.Now;

            var plinqTime = plinqEnd.Subtract(plinqStart).TotalMilliseconds;
            DisplayResults(plinqMin, plinqMax, plinqAverage, plinqTime);

            Console.WriteLine("\nPLINQ took " +
                $"{((linqTime - plinqTime) / linqTime):P0}" +
                " less time than LINQ");
        }

        static void DisplayResults(int min, int max, double average, double time)
        {
            Console.WriteLine($"Min: {min}\nMax: {max}\nAverage: {average:F}\nTotal time in milliseconds: {time:F}");
        }
    }
}