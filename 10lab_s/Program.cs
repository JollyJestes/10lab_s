
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            int arraySize = 1000000;
            int threadCount = 4;

            // Параллельное заполнение массива
            Movie[] movies = ParallelFillArray(arraySize, threadCount);

            // Измерение времени выполнения параллельной сортировки
            Stopwatch parallelSortStopwatch = Stopwatch.StartNew();
            ParallelSortArray(movies, threadCount);
            parallelSortStopwatch.Stop();

            // Измерение времени выполнения последовательной сортировки
            Movie[] sequentialMovies = ParallelFillArray(arraySize, 1);
            Stopwatch sequentialSortStopwatch = Stopwatch.StartNew();
            Array.Sort(sequentialMovies);
            sequentialSortStopwatch.Stop();

            // Вывод результатов
            Console.WriteLine($"Параллельная сортировка: {parallelSortStopwatch.ElapsedMilliseconds} мс");
            Console.WriteLine($"Последовательная сортировка: {sequentialSortStopwatch.ElapsedMilliseconds} мс");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }

    static Movie[] ParallelFillArray(int size, int threadCount)
    {
        try
        {
            Movie[] movies = new Movie[size];
            Parallel.For(0, threadCount, i =>
            {
                int chunkSize = size / threadCount;
                int startIndex = i * chunkSize;
                int endIndex = (i == threadCount - 1) ? size : (i + 1) * chunkSize;

                for (int j = startIndex; j < endIndex; j++)
                {
                    movies[j] = new Movie { Title = $"Movie{j}", Watched = GetRandomWatchedStatus() };
                }
            });

            return movies;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка при заполнении массива: {ex.Message}");
            return null;
        }
    }

    static void ParallelSortArray(Movie[] movies, int threadCount)
    {
        try
        {
            Parallel.For(0, threadCount, i =>
            {
                int chunkSize = movies.Length / threadCount;
                int startIndex = i * chunkSize;
                int endIndex = (i == threadCount - 1) ? movies.Length : (i + 1) * chunkSize;

                Array.Sort(movies, startIndex, endIndex - startIndex);
            });

            // Объединение отсортированных подмассивов
            Movie[] sortedMovies = new Movie[movies.Length];
            for (int i = 0; i < threadCount; i++)
            {
                int chunkSize = movies.Length / threadCount;
                int startIndex = i * chunkSize;
                int endIndex = (i == threadCount - 1) ? movies.Length : (i + 1) * chunkSize;

                Array.Copy(movies, startIndex, sortedMovies, startIndex, endIndex - startIndex);
            }

            Array.Sort(sortedMovies);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка при сортировке массива: {ex.Message}");
        }
    }

    static int GetRandomWatchedStatus()
    {
        Random random = new Random();
        return random.Next(0, 2);
    }
}