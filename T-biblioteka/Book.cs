using System.Text.Json;

public class Book
{
    //Book Class, containing all the info about the book
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public int Year { get; set; }
    public string Description { get; set; }
    public bool IsRead { get; set; }
    public bool IsFavorite { get; set; }

    public Book(string title, string author, string genre, int year, string description = "", bool isRead = false, bool isFavorite = false)
    {
        Title = title;
        Author = author;
        Genre = genre;
        Year = year;
        Description = description;
        IsRead = isRead;
        IsFavorite = isFavorite;
    }

    public override string ToString()
    {
        // Makes book.ToString() output the info of the book
        string status = IsRead ? "Прочитана" : "Не прочитана";
        string favorite = IsFavorite ? "* В избранном" : "";
        return $"   '{Title}' ({Year}): \nАвтор - {Author},  Жанр - [{Genre}], {status}, {favorite}";
    }
}