using System;
using System.Collections.Generic;
using System.Linq;

public class Library
{
    public List<Book> Books { get; set;} = new List<Book>();

    public void AddBook(Book book)
    {
        if (!Books.Any(b => b.Title.Equals(book.Title, StringComparison.OrdinalIgnoreCase)))
        {
            Books.Add(book);
            Console.WriteLine("Книга добавлена!");
        }
        else
        {
            Console.WriteLine("Книга с таким названием уже существует!");
        }
    }

    public void RemoveBook(string title)
    {
        var matchingBooks = Books.Where(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase)).ToList();

        if (matchingBooks.Count == 1)
        {
            Console.WriteLine($"Удалить книгу \"{title}\"? \n1 - да \n2 - нет");
            string choice = Console.ReadLine() ?? "";
            if (choice == "1")
            {
                Books.RemoveAll(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
                Console.WriteLine("Книга удалена.");
            }
            else
            {
                Console.WriteLine("Книга не была удалена.");
            }
        }
        else if (matchingBooks.Count > 1)
        {
            Console.WriteLine($"Найдено {matchingBooks.Count} книг с названием \"{title}\"");
            Console.WriteLine("Удалить все? \n1 - да \n2 - нет");
            string choice = Console.ReadLine() ?? "";
            if (choice == "1")
            {
                Books.RemoveAll(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
                Console.WriteLine("Все книги с этим названием удалены.");
            }
        }
        else
        {
            Console.WriteLine("Книги с таким названием не найдено.");
        }
    }

    public void ListBooks(string sortBy, List<Book> inputBooks)
    {
        IEnumerable<Book> sortedBooks;
        switch(sortBy.ToLower())
        {
            case "название":
                sortedBooks = inputBooks.OrderBy(b => b.Title);
                break;
            case "автор":
                sortedBooks = inputBooks.OrderBy(b => b.Author);
                break;
            case "жанр":
                sortedBooks = inputBooks.OrderBy(b => b.Genre);
                break;
            case "год":
                sortedBooks = inputBooks.OrderBy(b => b.Year);
                break;
            case "прочитано":
                sortedBooks = inputBooks.OrderBy(b => b.IsRead);
                break;
            case "в избранном":
                sortedBooks = inputBooks.OrderBy(b => b.IsFavorite);
                break;
            default:
                sortedBooks = inputBooks.OrderBy(b => b.Title);
                Console.WriteLine("Ошибка ввода. Автосортировка по названию... ");
                break;
        }
        if (!sortedBooks.Any()) 
        {
            Console.WriteLine("Нет книг для отображения.");
            return;
        }
        else
        {
            Console.WriteLine("Доступные книги: ");
        }

        foreach (var book in sortedBooks)
        {
            Console.WriteLine(book.ToString());
        }
    }

    public List<Book> Search(string key)
    {
        key = key.ToLower().Trim();
        return Books.Where(b =>
            b.Title.ToLower().Contains(key)).ToList();
    }

    public void SwitchIsReadStatus(string title)
    {
        var book = Books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (book != null)
        {
            book.IsRead = !book.IsRead;
            Console.WriteLine("Статус обновлён.");
        }
        else
        {
            Console.WriteLine("Этой книги нет в библиотеке");
        }
    }

    public void SwitchIsFavorite(string title)
    {
        var book = Books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (book != null)
        {
            book.IsFavorite = !book.IsFavorite;
            Console.WriteLine("Статус обновлён.");
        }
        else
        {
            Console.WriteLine("Этой книги нет в библиотеке");
        }
    }
    public List<Book> GetFavorites()
    {
        return Books.Where(b => b.IsFavorite).ToList();
    }

    public List<Book> GetUnread()
    {
        return Books.Where(b => !b.IsRead).ToList();
    }
}