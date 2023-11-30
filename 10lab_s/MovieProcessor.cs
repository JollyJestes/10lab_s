public class MovieProcessor
{
    public static void ParallelFillArray(Movie[] movies, int threadCount)
    {
        Parallel.For(0, threadCount, i =>
        {
            int chunkSize = movies.Length / threadCount;
            int startIndex = i * chunkSize;
            int endIndex = (i == threadCount - 1) ? movies.Length : (i + 1) * chunkSize;

            for (int j = startIndex; j < endIndex; j++)
            {
                movies[j] = new Movie { Title = $"Movie{j + 1}", Watched = GetRandomWatchedStatus() };
            }
        });
    }

    public static void ParallelSortArray(Movie[] movies, int threadCount)
    {
        Parallel.For(0, threadCount, i =>
        {
            int chunkSize = movies.Length / threadCount;
            int startIndex = i * chunkSize;
            int endIndex = (i == threadCount - 1) ? movies.Length : (i + 1) * chunkSize;

            Array.Sort(movies, startIndex, endIndex - startIndex);
        });

        Array.Sort(movies);
    }

    private static int GetRandomWatchedStatus()
    {
        Random random = new Random();
        return random.Next(0, 2);
    }
}