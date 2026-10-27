using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleBuilder
{
    // Директор управляет процессом создания статьи.
    public class ArticleDirector
    {
        public Article BuildFromFile(string fileName)
        {
            // Читаем все строки исходного TXT-файла.
            string[] lines = File.ReadAllLines(fileName);

            string title = "";
            string authorsLine = "";
            string hash = "";

            // Здесь будем собирать строки основного текста.
            List<string> textLines = new List<string>();

            // Этот флаг показывает, читаем ли мы сейчас текст статьи.
            bool readingText = false;

            // Перебираем все строки файла.
            foreach (string line in lines)
            {
                if (line.StartsWith("Заголовок:"))
                {
                    // Убираем метку "Заголовок:" и пробелы по краям.
                    title = line.Substring("Заголовок:".Length).Trim();
                }
                else if (line.StartsWith("Авторы:"))
                {
                    // Получаем строку с фамилиями авторов.
                    authorsLine = line.Substring("Авторы:".Length).Trim();
                }
                else if (line == "Текст:")
                {
                    // После этой строки начинается основной текст.
                    readingText = true;
                }
                else if (line.StartsWith("Хеш:"))
                {
                    // Получаем хеш и прекращаем чтение текста статьи.
                    hash = line.Substring("Хеш:".Length).Trim();
                    readingText = false;
                }
                else if (readingText)
                {
                    // Сохраняем очередную строку текста.
                    textLines.Add(line);
                }
            }

            // Объединяем строки статьи, сохраняя переносы строк.
            string text = string.Join(
                Environment.NewLine, textLines);

            // Разделяем фамилии авторов по запятой.
            List<string> authors = new List<string>();

            foreach (string author in authorsLine.Split(','))
            {
                // Не добавляем пустые строки.
                if (author.Trim() != "")
                {
                    authors.Add(author.Trim());
                }
            }

            // Создаём статью с помощью Builder.
            // Методы вызываются последовательно.
            Article article = new ArticleBuilder()
                .SetTitle(title)
                .SetAuthors(authors)
                .SetText(text)
                .SetHash(hash)
                .CheckHash()
                .Build();

            return article;
        }
    }
}
