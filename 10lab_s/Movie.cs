public class Movie : IComparable<Movie>
{
    public string Title { get; set; }
    public int Watched { get; set; }

    public int CompareTo(Movie other)
    {
        return string.Compare(Title, other.Title, StringComparison.Ordinal);
    }

    public override string ToString()
    {
        return $"{Title} - Смотрел: {(Watched == 1 ? "Да" : "Нет")}";
    }
}