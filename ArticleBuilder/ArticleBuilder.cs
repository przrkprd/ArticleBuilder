using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ArticleBuilder
{
    // Класс ArticleBuilder постепенно создаёт объект Article.
    public class ArticleBuilder
    {
        // Объект статьи, который будем заполнять.
        private Article article = new Article();

        // Записываем заголовок статьи.
        public ArticleBuilder SetTitle(string title)
        {
            article.Title = title;
            return this;
        }

        // Записываем фамилии авторов.
        public ArticleBuilder SetAuthors(
            System.Collections.Generic.List<string> authors)
        {
            article.Authors = authors;
            return this;
        }

        // Записываем основной текст.
        public ArticleBuilder SetText(string text)
        {
            article.Text = text;
            return this;
        }

        // Записываем хеш из исходного файла.
        public ArticleBuilder SetHash(string hash)
        {
            article.Hash = hash;
            return this;
        }

        // Проверяем, совпадает ли хеш с текстом статьи.
        public ArticleBuilder CheckHash()
        {
            // SHA256 вычисляет хеш по переданным байтам.
            using (SHA256 sha = SHA256.Create())
            {
                // Преобразуем текст статьи в байты UTF-8.
                byte[] bytes = Encoding.UTF8.GetBytes(article.Text);

                // Вычисляем SHA-256.
                byte[] hashBytes = sha.ComputeHash(bytes);

                // Преобразуем байты хеша в строку из шестнадцатеричных символов.
                string calculatedHash =
                    BitConverter.ToString(hashBytes).Replace("-", "");

                // Сравниваем вычисленный хеш с хешем из файла.
                // Игнорируем различия между большими и маленькими буквами.
                article.HashValid = string.Equals(
                    calculatedHash,
                    article.Hash,
                    StringComparison.OrdinalIgnoreCase);
            }

            // Возвращаем текущий Builder, чтобы можно было
            // продолжить вызовы методов одной цепочкой.
            return this;
        }

        // Завершаем создание статьи и возвращаем готовый объект.
        public Article Build()
        {
            return article;
        }
    }
}
