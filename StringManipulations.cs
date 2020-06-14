using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_Theme_05
{
    // Задание 2.
    // 1. Создать метод, принимающий  текст и возвращающий слово, содержащее минимальное количество букв
    // Пример: Текст: "A ББ ВВВ ГГГГ ДДДД  ДД ЕЕ ЖЖ ЗЗЗ"
    // Ответ: А

    // 2.* Создать метод, принимающий  текст и возвращающий слово(слова) с максимальным количеством букв 
    // Примечание: слова в тексте могут быть разделены символами (пробелом, точкой, запятой) 
    // Пример: Текст: "A ББ ВВВ ГГГГ ДДДД  ДД ЕЕ ЖЖ ЗЗЗ"
    // 2. ГГГГ, ДДДД

    // Задание 3. Создать метод, принимающий текст. 
    // Результатом работы метода должен быть новый текст, в котором
    // удалены все кратные рядом стоящие символы, оставив по одному 
    // Пример: ПППОООГГГООООДДДААА >>> ПОГОДА
    // Пример: Ххххоооорррооошшшиий деееннннь >>> хороший день

    class StringManipulations
    {
        int winHeight = 20;             // Высота экрана (для меню) 20 строк
        int winWidth = 80;              // Ширина экрана (для меню) 80 строк
        string inputString;
        string result;
        SimpleParser simpleParser;

        public StringManipulations()
        {
            simpleParser = new SimpleParser();  
        }

        /// <summary>
        /// Создаёт окно меню для ввода параметров для вызова метода FindShortestWord
        /// </summary>
        public void MenuShortestWord(WindowOutput window)
        {
            window.newWindow(winWidth, winHeight);
            window.HeaderCenter("НАЙТИ САМОЕ КОРОТКОЕ СЛОВО", winWidth, 2, ConsoleColor.Yellow);
            window.HeaderCenter("символы разделители - пробел, запятая и точка", winWidth, 3, ConsoleColor.Gray);

            Console.CursorVisible = true;
            Console.SetCursorPosition(2, 5);
            Console.Write("Введите строку: ");

            inputString = Console.ReadLine();
            Console.WriteLine(FindShortestWord(inputString));

            Console.CursorVisible = false;

            window.HeaderCenter("НАЖМИТЕ ЛЮБУЮ КЛАВИШУ", winWidth, Console.CursorTop + 2, ConsoleColor.DarkYellow);
            Console.ReadKey();
        }

        /// <summary>
        /// Возвращает самое короткое слово из строки. Слова разделены символами пробел, точка и запятая.
        /// </summary>
        /// <param name="inputString">Любая строка символов</param>
        /// <returns></returns>
        public string FindShortestWord(string inputString)
        {
            string minWord;
            (string word, int pos) nextWord = ("", 0);
            int i = 0;
            
            if (inputString.Length == 0) return $"Введна пустая строка";

            nextWord = simpleParser.FindNextWord(inputString, i);   // ищем первое слово
            minWord = nextWord.word;            // делаем его минимальным        
            i = nextWord.pos;                   // помним, что i увеличивается

            if (minWord.Length == 1)
                return $"Кратчайшее слово: {minWord} длина 1 символ";

            while (i < inputString.Length)
            {
                nextWord = simpleParser.FindNextWord(inputString, i);  // ищем следующее слово 
                i = nextWord.pos;                         // помним, что i увеличивается

                if (nextWord.word.Length == 1)
                {
                    return $"Кратчайшее слово: {nextWord.word} длина 1 символ";
                }
                else if (nextWord.word != "" & nextWord.word.Length < minWord.Length)  // и сравниваем их длину
                {
                    minWord = nextWord.word;   // если длина найденного слова меньше, 
                                               // то найденное становится искомым словом
                    nextWord = ("", 0);        // обнуляем следующее слово
                }
            }

            if (minWord.Length == 0) return "Введёная строка состоит из разделителей";

            // эта проверка нужна, если самое короткое слово последнее в строке
            if (nextWord.word != "" & nextWord.word.Length < minWord.Length)
                minWord = nextWord.word;

            return $"Кратчайшее слово: {minWord} длина {minWord.Length} символов";
        }

        // Задание 2.
        // 2.* Создать метод, принимающий  текст и возвращающий слово(слова) с максимальным количеством букв 
        // Примечание: слова в тексте могут быть разделены символами (пробелом, точкой, запятой) 
        // Пример: Текст: "A ББ ВВВ ГГГГ ДДДД  ДД ЕЕ ЖЖ ЗЗЗ"
        // 2. ГГГГ, ДДДД

        /// <summary>
        /// Создаёт окно меню для ввода параметров для вызова метода FindLongestWords
        /// </summary>
        public void MenuLongestWords(WindowOutput window)
        {
            window.newWindow(winWidth, winHeight);
            window.HeaderCenter("НАЙТИ ВСЕ САМЫЕ ДЛИННЫЕ СЛОВА", winWidth, 2, ConsoleColor.Yellow);
            window.HeaderCenter("символы разделители - пробел, запятая и точка", winWidth, 3, ConsoleColor.Gray);

            Console.CursorVisible = true;
            Console.SetCursorPosition(2, 5);
            Console.Write("Введите строку: ");

            inputString = Console.ReadLine();
            Console.WriteLine(FindLongestWords(inputString));

            Console.CursorVisible = false;

            window.HeaderCenter("НАЖМИТЕ ЛЮБУЮ КЛАВИШУ", winWidth, Console.CursorTop + 2, ConsoleColor.DarkYellow);
            Console.ReadKey();
        }

        /// <summary>
        /// Возвращает самое длинное слово(слова) из строки. Слова разделены символами пробел, точка и запятая.
        /// </summary>
        /// <param name="inputString">Любая строка символов</param>
        /// <returns></returns>
        public string FindLongestWords(string inputString)
        {
            (string word, int pos) nextWord; // очередное найденное слово и его позиция
            string result = "";              // строка со всеми самыми длинными словами
            int max = 0;                     // текущая длина самого длинного слова
            int i = 0;                       // текущая позиция в строке

            if (inputString.Length == 0) return "Введна пустая строка";

            nextWord = simpleParser.FindNextWord(inputString, i);         // ищем первое слово в строке
            result = nextWord.word;             // пока результат - это найденное слово                    
            max = result.Length;                // 
            i = nextWord.pos;                   // помним, что i увеличивается

            while (i < inputString.Length)
            {
                nextWord = simpleParser.FindNextWord(inputString, i);  // ищем следующее слово 
                i = nextWord.pos;                         // помним, что i увеличивается

                // и сравниваем их длину
                if (nextWord.word != "" & nextWord.word.Length > max) // нашли слово длиннее
                {
                    result = nextWord.word;   // если длина найденного слова больше максимальной
                                              // на данный момент, то обновляем строку результата
                    max = result.Length;      // делаем новую длину макс слова
                    nextWord = ("", 0);       // обнуляем следующее слово
                }

                if (nextWord.word != "" & nextWord.word.Length == max) // нашли слово такой же макс длины
                {
                    result += result != "" ? ", " + nextWord.word : nextWord.word;
                }
            }

            if (result.Length == 0) return "Введёная строка состоит из разделителей";

            return $"Самые длинные слова (длина {max} символов) : {result}";
        }

        // Задание 3. Создать метод, принимающий текст. 
        // Результатом работы метода должен быть новый текст, в котором
        // удалены все кратные рядом стоящие символы, оставив по одному 
        // Пример: ПППОООГГГООООДДДААА >>> ПОГОДА
        // Пример: Ххххоооорррооошшшиий деееннннь >>> хороший день

        /// <summary>
        /// Создаёт окно меню для ввода параметров для вызова метода RefineString
        /// </summary>
        public void MenuRefineString(WindowOutput window)
        {
            window.newWindow(winWidth, winHeight);
            window.HeaderCenter("УДАЛИТЬ ПОВТОРЯЮЩИЕСЯ БУКВЫ", winWidth, 2, ConsoleColor.Yellow);
            window.HeaderCenter("", winWidth, 3, ConsoleColor.Yellow);

            Console.CursorVisible = true;
            Console.SetCursorPosition(2, 5);
            Console.Write("Введите строку: ");

            inputString = Console.ReadLine();
            Console.WriteLine(RefineString(inputString));

            Console.CursorVisible = false;

            window.HeaderCenter("НАЖМИТЕ ЛЮБУЮ КЛАВИШУ", winWidth, Console.CursorTop + 2, ConsoleColor.DarkYellow);
            Console.ReadKey();
        }
        /// <summary>
        /// Удаляем все кратные рядом стоящие символы, оставляя лишь один экземпляр
        /// </summary>
        /// <param name="inputString">Любая строка символов</param>
        /// <returns></returns>
        public string RefineString(string inputString)
        {
            int i;                  // текущая позиция в строке
            string result = "";     // результирующая строка
            char symbol;            // предыдущий символ

            if (inputString.Length == 0) return "Введна пустая строка";
            if (inputString.Length == 1) return "Результат: " + inputString;

            symbol = inputString[0];
            result = $"{symbol}";

            for (i = 1; i < inputString.Length; i++)
                if (symbol != inputString[i])
                {
                    symbol = inputString[i];
                    result += $"{symbol}";
                }

            return "Результат: " + result;
        }
    }   // class StringManipulations

}   // namespace Homework_Theme_05
