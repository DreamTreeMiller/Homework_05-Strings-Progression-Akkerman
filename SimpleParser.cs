using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_Theme_05
{
    /// <summary>
    /// Класс - примитивный парсер, содержащий два вспомогательных метода
    /// и список символов разделителей
    /// </summary>
    class SimpleParser
    {
        public char[] separators = { ' ', ',', '.' };

        /// <summary>
        /// Проверяет, является ли текущий символ разделителем
        /// </summary>
        /// <param name="checkChar">Проверяемый символ</param>
        /// <returns></returns>
        public bool IsSeparator(char checkChar)
        {
            foreach (var e in separators)
                if (checkChar == e) return true;
            return false;
        }

        /// <summary>
        /// Перегруз IsSeparator c явным заданием массива разделителей
        /// Проверяет, является ли текущий символ разделителем, принадлежащим массиву char[] разделителей
        /// </summary>
        /// <param name="separators">Массив символов разделителей</param>
        /// <param name="checkChar">Проверяемый символ</param>
        /// <returns></returns>
        public bool IsSeparator(char[] separators, char checkChar)
        {
            foreach (var e in separators)
                if (checkChar == e) return true;
            return false;
        }

        /// <summary>
        /// Ищет в строке inputString следующее слово в строке, начиная с позиции i.
        /// Возвращает это слово и позицию в строке на следующий символ после слова.
        /// Если достигнут конец строки, позиция равна длине строки.
        /// Если в строке больше нет слов, а только разделители, то возвращается пустая строка.
        /// </summary>
        /// <param name="inputString">Исходная строка</param>
        /// <param name="i">Позиция в строке, с которой надо искать слово</param>
        /// <returns></returns>
        public (string, int) FindNextWord(string inputString, int i)
        {
            string currWord = "";

            for (; i < inputString.Length; i++)
                if (!IsSeparator(inputString[i])) break;

            for (; i < inputString.Length; i++)
                if (!IsSeparator(inputString[i]))
                {
                    currWord += $"{inputString[i]}";
                }
                else break;

            return (currWord, i);
        }   //  (string, int) FindNextWord(string, int)

        /// <summary>
        /// Перегруз метода с явным указателем массива разделителей
        /// Ищет в строке inputString следующее слово в строке, начиная с позиции i.
        /// Возвращает это слово и позицию в строке на следующий символ после слова.
        /// Если достигнут конец строки, позиция равна длине строки.
        /// Если в строке больше нет слов, а только разделители, то возвращается пустая строка.
        /// </summary>
        /// <param name="inputString">Исходная строка</param>
        /// <param name="separators">Массив разделителей</param>
        /// <param name="i">Позиция в строке, с которой надо искать слово</param>
        /// <returns></returns>
        public (string, int) FindNextWord(string inputString, char[] separators, int i)
        {
            string currWord = "";

            for (; i < inputString.Length; i++)
                if (!IsSeparator(separators, inputString[i])) break;

            for (; i < inputString.Length; i++)
                if (!IsSeparator(separators, inputString[i]))
                {
                    currWord += $"{inputString[i]}";
                }
                else break;

            return (currWord, i);
        }   //  (string, int) FindNextWord(string, char[], int)

        /// <summary>
        /// Просит ввести число в заданном диапазоне, включая крайние значения.
        /// Реализована "защита от дурака"
        /// Возвращает введённое число
        /// </summary>
        /// <param name="introMessage">Сообщение, что надо ввести</param>
        /// <param name="outOfrangeMsg">Сообщение о выходе за пределы диапазона</param>
        /// <param name="minValue">Нижнее значение диапазона</param>
        /// <param name="maxValue">Вержнее значение диапазона</param>
        /// <param name="x">Столбец консоли, с которого выводить строку сообщения</param>
        /// <param name="y">Строка консоли, с которой выводить строку сообщения</param>
        /// <returns></returns>
        public int AskAndParse(string introMessage, string outOfrangeMsg,
                         int minValue, int maxValue, int x, int y)
        {
            int value;          // возвращаемое значение
            bool isNumber;      // флаг, введено ли число, а не просто набор символов
            bool isInRange;     // флаг, введённое число находятся в заданном диапазоне 

            string msgToPrint;     // Выводимое сообщение, сформированное на основе переданных значений
            string inputMsg = "";  // Вводимая строка
            string errorMsg;       // Сообщение об ошибке

            #region Вводим число
            // Осуществляем "защиту от дурака" при вводе числа
            msgToPrint = $"{introMessage} от {minValue} до {maxValue:#,###,###,###}: ";
            errorMsg = "";
            do
            {
                Console.SetCursorPosition(x, y);
                Console.Write(msgToPrint + "".PadRight(inputMsg.Length, ' '));
                Console.SetCursorPosition(x + msgToPrint.Length, y);
                inputMsg = Console.ReadLine();
                isNumber = int.TryParse(inputMsg, out value);  //  вводим кол-во игроков
                isInRange = true;  // предполагаем, что оно в диапазоне от 2 до 10

                if (isNumber)     // введено числов ?
                {
                    // введено число, но надо проверить,
                    // оно в рамках диапазона minValue - maxValue
                    if (value < minValue || maxValue < value)
                    {
                        // Если на предыдущей итерации были введены неверные данные, то отались сообщения об ошибке,
                        // которые надо почистить
                        if (errorMsg != "")
                        {
                            Console.SetCursorPosition(x, y + 1);
                            Console.Write("".PadRight(errorMsg.Length, ' '));
                        }
                        Console.SetCursorPosition(x, y + 1);
                        errorMsg = $"Ошибка! {outOfrangeMsg} от {minValue} до {maxValue:#,###,###,###}!";
                        Console.WriteLine(errorMsg);
                        isInRange = false;
                    }
                }
                else
                {
                    // Если на предыдущей итерации были введены неверные данные, то отались сообщения об ошибке,
                    // которые надо почистить
                    if (errorMsg != "")
                    {
                        Console.SetCursorPosition(x, y + 1);
                        Console.Write("".PadRight(errorMsg.Length, ' '));
                    }
                    Console.SetCursorPosition(x, y + 1);
                    errorMsg = "Ошибка! Вы должны ввести число!";
                    Console.WriteLine(errorMsg);
                }

            } while (!isNumber || !isInRange);    // Если введено не число, или число вне диапазона
                                                  // Вводим число заново

            // Если были введены неверные данные, то отались сообщения об ошибке,
            // которые надо почистить
            if (errorMsg != "")
            {
                Console.SetCursorPosition(x, y + 1);
                Console.Write("".PadRight(errorMsg.Length, ' '));
            }

            #endregion
            // на выходе переменная value содержит введённое значение
            return value;
        }

        /// <summary>
        /// Возвращает true, если введена корректная или пустая последовательность целых чисел
        /// false, если хотя бы один элемент последовательности не целое число
        /// Разделителем чисел являются пробел или запятая. Точки не допускаются
        /// </summary>
        /// <param name="inputString">Строка чисел</param>
        /// <param name="sequence">Возвращаемый массив корректно введёных целых чисел из строки</param>
        /// <returns></returns>
        public bool StringToNumbersConverter(string inputString, out int[] sequence)
        {
            char[] separators = { ' ', ',' };   // массив разделителей

            bool isInputCorrect = true;         // последовательность введена корректно?
            int sequenceLength = 0;             // количество введённых чисел
            int[] temp;                         // временный массив для введённых чисел

            (string word, int pos) newWord;     // считываемое слово, которое надо преобразовать в число
            int currNum;                        // текущее число
            int i = 0;                          // текущий символ в обрабатываемой строке

            // Генерируем максимально возможный массив для чисел
            // Крайние случай - все числа однозначные и разделены одним пробелом
            // Тогда для строки "1 2 3" длиной 5 символов надо зарезервировать массив из 3-х чисел
            // В случае пустой строки, длина массива будет равна 0
            temp = new int[inputString.Length / 2 + inputString.Length % 2];

            for (i = 0; i < inputString.Length; i++)
            {
                // ищем очередное слово в строке
                newWord = FindNextWord(inputString, separators, i);
                i = newWord.pos;

                // Проверяем, является ли очередное найденное слово целым числом
                if (int.TryParse(newWord.word, out currNum))    // да, целое число
                {
                    temp[sequenceLength] = currNum;
                    sequenceLength++;                           // увеличиваем счётчик чисел
                }
                else
                {
                    sequence = new int[0];
                    return false;

                }   // if(int.TryParse(newWord.word, out currNum)) 
            }   // for (i = 0; i < inputString.Length; i++)

            sequence = new int[sequenceLength];

            if (sequenceLength > 0)
            {
                Array.Copy(temp, sequence, sequenceLength);
            }

            return isInputCorrect;
        }   // int[] StringToNumbersConverter(string inputString)

        // Пытался работать с динамическим списком, но не знаю, как выдлять память для нового эл-та
        //unsafe struct ListOfnumbers
        //{
        //    public int num;
        //    public ListOfnumbers* nextItemPointer;
        //}

    }   // class SimpleParser
}
