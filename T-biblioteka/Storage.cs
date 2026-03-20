using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

public static class Storage
{
    private const string data_file = "books.json";

    public static void SaveBooks(List<Book> books)
    {
        try
        { 
            //Trying to save all books into json file
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase // Optional: for consistency
            };
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
                string jsonString = File.ReadAllText(data_file, Encoding.UTF8);
                
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                
                var books = JsonSerializer.Deserialize<List<Book>>(jsonString, options);
                Console.WriteLine("Данные загружены успешно! \n");
                return books ?? new List<Book>();
            }
            
            Console.WriteLine("Файл библиотеки не найден. Создаю новую библиотеку...");
            return new List<Book>();
        }
        catch (JsonException jsonEx)
        {
            Console.WriteLine($"Ошибка чтения файла: {jsonEx.Message}");
            return new List<Book>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки: {ex.Message}");
            return new List<Book>();
        }
    }
}