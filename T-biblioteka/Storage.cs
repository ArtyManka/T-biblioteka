using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class Storage
{
    private const string data_file = "books.json";

    public static void SaveBooks(List<Book> books)
    {
        try
        { 
            //Trying to save all books into json file
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(books, options);
            File.WriteAllText(data_file, jsonString);
        }
        catch (Exception ex)
        {
            //Error message
            Console.WriteLine($"Ошибка сохранения: {ex.Message}");
        }
    }
    public static List<Book> LoadBooks()
    {
        try
        {
            if (File.Exists(data_file))
            {
                string jsonString = File.ReadAllText(data_file);
                var books = JsonSerializer.Deserialize<List<Book>>(jsonString);
                Console.WriteLine("Загруженно успешно! \n");
                return books ?? new List<Book>(); // If books == Null => return empty, else => return the list
            }
            Console.WriteLine("Файл библиотеки не найден. Создаю новую библиотеку...");
            Console.WriteLine("Создано успешно! \n");
            return new List<Book>(); //If file don't exist
        }
        catch (Exception ex)
        {
            //Error message + return empty list
            Console.WriteLine($"Ошибка загрузки: {ex.Message}");
            return new List<Book>();
        }
    }
}