using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleBuilder
{
    // Класс Article хранит всю информацию о статье.
    // Объект этого класса мы в итоге преобразуем в JSON.
    public class Article
    {
        // Заголовок статьи.
        public string Title { get; set; }

        // Список фамилий авторов.
        public List<string> Authors { get; set; }

        // Основной текст статьи.
        public string Text { get; set; }

        // Хеш, который был указан в TXT-файле.
        public string Hash { get; set; }

        // Результат проверки хеша.
        // true — хеш правильный, false — неправильный.
        public bool HashValid { get; set; }
    }
}
