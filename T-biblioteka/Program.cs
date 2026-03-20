using System;
using System.Linq;

class Program
{
    private static Library library = new Library();

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;
        
        var loadedBooks = Storage.LoadBooks();
        library.Books = loadedBooks;

        Console.WriteLine("Добро пожаловать в T-Библиотеку!");

        while (true)
        {
            ShowMenu();
            Console.WriteLine();
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    AddBook();
                    break;
                case "2":
                    ViewBooks();
                    break;
                case "3":
                    SearchBooks();
                    break;
                case "4":
                    SwitchReadStatus();
                    break;
                case "5":
                    SwitchFavoriteStatus(); 
                    break;
                case "6":
                    ShowFavorites(); //asdasdas
                    break;
                case "7":
                    ShowUnread(); //dfgsdfg
                    break;
                case "8":
                    RemoveBook();
                    break;
                case "9":
                    LoadLibrary();
                    break;
                case "0":
                    SaveAndExit();
                    return;
                default:
                    Console.WriteLine("Некорректный ввод!");
                    break;
            }
        }
    }
    static void ShowFavorites()
    {
        var favBooks = library.GetFavorites();
        if (!favBooks.Any()) 
        {
            Console.WriteLine("Нет книг для отображения.");
            return;
        }
        else
        {
            Console.WriteLine("Доступные книги: ");
        }

        foreach (var book in favBooks)
        {
            Console.WriteLine(book.ToString());
        }
    }

    static void ShowUnread()
    {
        var unrBooks = library.GetUnread();
        if (!unrBooks.Any()) 
        {
            Console.WriteLine("Нет книг для отображения.");
            return;
        }
        else
        {
            Console.WriteLine("Доступные книги: ");
        }

        foreach (var book in unrBooks)
        {
            Console.WriteLine(book.ToString());
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("\nНажмите любую кнопку чтобы продолжить");
        Console.ReadLine();
        Console.WriteLine("\n=== T-Библиотека ===");
        Console.WriteLine("1. Добавить книгу");
        Console.WriteLine("2. Просмотр всех книг");
        Console.WriteLine("3. Поиск книги");
        Console.WriteLine("4. Изменить статус 'прочитана'");
        Console.WriteLine("5. Добавить/убрать из избранного");
        Console.WriteLine("6. Показать избранные");
        Console.WriteLine("7. Показать непрочитанные книги");
        Console.WriteLine("8. Удалить книгу");
        Console.WriteLine("9. Загрузить библиотеку");
        Console.WriteLine("0. Выйти и сохранить");
        Console.Write("Выберите действие: ");
    }

    static void AddBook()
{
    try
    {
        Console.Write("Название: ");
        string title = Console.ReadLine() ?? "";

        if (library.Search(title).Any())
            Console.WriteLine("Книга с этим названием уже есть. Новая книга не добавлена.");
        else
        {
            Console.Write("Автор: ");
            string author = Console.ReadLine() ?? "";

            Console.Write("Жанр: ");
            string genre = Console.ReadLine() ?? "";

            Console.Write("Год: ");
            int year = GetYearFromUser(); 

            Console.Write("Описание (не обязательно): ");
            string description = Console.ReadLine() ?? "";

            var book = new Book(title, author, genre, year, description);
            library.AddBook(book);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка добавления книги: {ex.Message}");
    }
}

    static int GetYearFromUser()
    {
        int year = 0;
        int attempts = 0;
        bool success = false;
        
        while (!success && attempts < 3)
        {
            if (attempts > 0)
            {
                Console.WriteLine($"Попробуйте еще раз (попытка {attempts + 1}/3):");
            }
            
            string input = Console.ReadLine() ?? "";
            if (int.TryParse(input, out year))
            {
                success = true;
            }
            else
            {
                Console.WriteLine("Неправильно указан год. Используйте только цифры.");
                attempts++;
            }
        }
        
        if (!success)
        {
            Console.WriteLine("Неправильно указан год. Автоматически был выбран год - 0.");
            year = 0;
        }
        
        return year;
    }
    static void ViewBooks()
    {
        Console.Write("Сортировка по (название/автор/жанр/год/прочитано/в избранном): ");
        string sortKey = Console.ReadLine() ?? "";
        // if (string.IsNullOrWhiteSpace(sortKey)) sortKey = "название"; //USE IF ERROR OCURS

        library.ListBooks(sortKey, library.Books);
    }

    static void SearchBooks()
    {
        Console.Write("Введите ключевое слово: ");
        string keyword = Console.ReadLine() ?? "";
        Console.WriteLine("Поиск книг с похожим названием...");
        Console.Write("Сортировка по (название/автор/жанр/год/прочитано/в избранном): ");
        string sortCriteria = Console.ReadLine() ?? "";

        var results = library.Search(keyword); //search
        Console.WriteLine($"\nНайдено {results.Count} книг:");
        library.ListBooks(sortCriteria, results); //list sorted books
    }

    static void SwitchReadStatus()
    {
        Console.Write("Введите название книги: ");
        string title = Console.ReadLine() ?? "";
        var results = library.Search(title);

        if (results.Count == 0)
        {
            Console.WriteLine("Книга не найдена!");
            return;
        }

        library.SwitchIsReadStatus(title);
    }

    static void SwitchFavoriteStatus()
    {
        Console.Write("Введите название книги: ");
        string title = Console.ReadLine() ?? "";
        var results = library.Search(title);

        if (results.Count == 0)
        {
            Console.WriteLine("Книга не найдена!");
            return;
        }
        
        library.SwitchIsFavorite(title);
    }

    static void RemoveBook()
    {
        Console.Write("Введите название книги для удаления: ");
        string title = Console.ReadLine() ?? "";
        library.RemoveBook(title);
    }

    static void SaveAndExit()
    {
        Storage.SaveBooks(library.Books);
        Console.WriteLine("Данные сохранены. До свидания!");
    }

    static void LoadLibrary()
    {
        library.Books = Storage.LoadBooks();
    }
}
