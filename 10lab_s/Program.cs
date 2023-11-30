using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            int arraySize = 1000000;
            int threadCount = 4;

            Movie[] movies = new Movie[arraySize];
            MovieProcessor.ParallelFillArray(movies, threadCount);

            Stopwatch parallelSortStopwatch = Stopwatch.StartNew();
            MovieProcessor.ParallelSortArray(movies, threadCount);
            parallelSortStopwatch.Stop();

            Movie[] sequentialMovies = new Movie[arraySize];
            MovieProcessor.ParallelFillArray(sequentialMovies, 1);
            Stopwatch sequentialSortStopwatch = Stopwatch.StartNew();
            Array.Sort(sequentialMovies);
            sequentialSortStopwatch.Stop();

            Console.WriteLine($"Параллельная сортировка: {parallelSortStopwatch.ElapsedMilliseconds} мс");
            Console.WriteLine($"Последовательная сортировка: {sequentialSortStopwatch.ElapsedMilliseconds} мс");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}