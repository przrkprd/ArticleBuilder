using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArticleBuilder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            // Имя входного TXT-файла.
            string inputFile = "article.txt";

            // Имя выходного JSON-файла.
            string outputFile = "article.json";

            try
            {
                // Создаём директора.
                ArticleDirector director = new ArticleDirector();

                // Читаем TXT и получаем готовый объект статьи.
                Article article = director.BuildFromFile(inputFile);

                // Преобразуем объект статьи в формат JSON.
                string json = JsonSerializer.Serialize(
                    article,
                    new JsonSerializerOptions
                    {
                        // Форматируем JSON с отступами для удобства чтения.
                        WriteIndented = true
                    });

                // Сохраняем JSON в файл.
                File.WriteAllText(outputFile, json);

                // Выводим результат проверки хеша.
                if (article.HashValid)
                {
                    Console.WriteLine("Хеш-код правильный.");
                }
                else
                {
                    Console.WriteLine("Ошибка: хеш-код не совпадает с текстом!");
                }

                Console.WriteLine("JSON сохранён в файле " + outputFile);
            }
            catch (Exception ex)
            {
                // Если файл отсутствует или возникла другая ошибка,
                // выводим её описание.
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }

            // Не закрываем консоль сразу после выполнения.
            Console.ReadKey();
        }
    }
}
