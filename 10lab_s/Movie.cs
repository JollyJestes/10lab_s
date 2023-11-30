public class Movie : IComparable<Movie>
{
    public string Title { get; set; }
    public int Watched { get; set; }

    public int CompareTo(Movie other)
    {
        // Извлечение числа из названия и сортировка по нему
        int thisNumber = int.Parse(new string(Title.Where(char.IsDigit).ToArray()));
        int otherNumber = int.Parse(new string(other.Title.Where(char.IsDigit).ToArray()));

        return thisNumber.CompareTo(otherNumber);
    }

    public override string ToString()
    {
        return $"{Title} - Смотрел: {(Watched == 1 ? "Да" : "Нет")}";
    }
}