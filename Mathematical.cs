using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_Theme_05
{
    // Задание 4. Написать метод принимающий некоторое количесво чисел, выяснить
    // является заданная последовательность элементами арифметической или геометрической прогрессии
    // 
    // Примечание
    //             http://ru.wikipedia.org/wiki/Арифметическая_прогрессия
    //             http://ru.wikipedia.org/wiki/Геометрическая_прогрессия
    //


    class Mathematical
    {
        int winHeight = 20;             // Высота экрана (для меню) 20 строк
        int winWidth = 80;              // Ширина экрана (для меню) 80 строк
        SimpleParser simpleParser;

        public Mathematical()
        {
            simpleParser = new SimpleParser();
        }

        // Задание 4. Написать метод принимающий некоторое количесво чисел, выяснить
        // является заданная последовательность элементами арифметической или геометрической прогрессии
        // 
        // Примечание
        //             http://ru.wikipedia.org/wiki/Арифметическая_прогрессия
        //             http://ru.wikipedia.org/wiki/Геометрическая_прогрессия
        //

        /// <summary>
        /// Метод ввода последовательности. 
        /// </summary>
        /// <param name="window"></param>
        public void MenuIsProgression(WindowOutput window)
        {
            char[] separators = { ' ', ',' };
            string inputString;                 // вводимая строка чисел
            bool isCorrectString;               // Введены ли числа корректно
            int[] numberSequence;               // введённая последовательность чисел
            int currY;

            window.newWindow(winWidth, 40);
            window.HeaderCenter("ЯВЛЯЕТСЯ ЛИ ВВЕДЁННАЯ ПОСЛЕДОВАТЕЛЬНОСТЬ ПРОГРЕССИЕЙ", winWidth, 2, ConsoleColor.Yellow);
            window.HeaderCenter("АРИФМЕТИЧЕСКОЙ ИЛИ ГЕОМЕТРИЧЕСКОЙ", winWidth, 3, ConsoleColor.Yellow);
            window.HeaderCenter("Введите минимум 3 числа, разделяя их пробелом или запятой.", winWidth, 5, ConsoleColor.White);
            window.HeaderCenter("Допустимы только целые числа. Порядок введения чисел не важен", winWidth, 6, ConsoleColor.White);

            Console.CursorVisible = true;

            do
            {
                Console.SetCursorPosition(2, 8);
                Console.Write("Введите целые числа. По окончании нажмите ВВОД: ");

                inputString = Console.ReadLine();

                isCorrectString = simpleParser.StringToNumbersConverter(inputString, out numberSequence);

                if (isCorrectString)
                {
                    if (numberSequence.Length < 3)
                    {
                        currY = Console.CursorTop;
                        window.HeaderCenter("Количесво корректно введёных целых чисел меньше 3 !",
                                             winWidth, currY + 1, ConsoleColor.White);
                        window.HeaderCenter("Нажмите любую клавишу и повторите ввод.",
                                             winWidth, currY + 2, ConsoleColor.White);
                        Console.ReadKey();
                        currY = currY + 3;

                        for (int i = 8; i < currY; i++)
                            window.CleanLine(winWidth, i, ConsoleColor.Black);
                    }
                }
                else
                {
                    currY = Console.CursorTop;
                    window.HeaderCenter("Некоторые числа введены не корректно !",
                                         winWidth, currY + 1, ConsoleColor.White);
                    window.HeaderCenter("Нажмите любую клавишу и повторите ввод.",
                                         winWidth, currY + 2, ConsoleColor.White);
                    Console.ReadKey();
                    currY = currY + 3;

                    for (int i = 8; i < currY; i++)
                        window.CleanLine(winWidth, i, ConsoleColor.Black);
                }
            } while (numberSequence.Length < 3);

            currY = Console.CursorTop;
            window.HeaderCenter("Данная последовательность",
                                 winWidth, currY + 1, ConsoleColor.White);
            window.HeaderCenter(IsProgression(numberSequence),
                                 winWidth, currY + 2, ConsoleColor.White);

            window.HeaderCenter("НАЖМИТЕ ЛЮБУЮ КЛАВИШУ", winWidth, Console.CursorTop + 2, ConsoleColor.DarkYellow);
            Console.ReadKey();

        }   // public void MenuIsProgression(WindowOutput window)
 
        // Решение
        // Задачу имеет смысл решать только при добавлении условия, 
        // что ВСЕ заданные числа являются СОСЕДНИМИ числа последовательности
        // либо арифметической, либо геометрической

        // Если допустить, что между двумя соседними по условию числами, есть ещё числа
        // то эти числа ВСЕГДА будут являться частью арифметической и геометрической прогрессий
        // Для арифм. прогрессии (АП) шагом прогрессии может станет НОД всех разностей соседних по условию чисел
        // Для геом. прогрессии (ГП) вот здесь http://ru.wikipedia.org/wiki/Геометрическая_прогрессия 
        // сказано, что логарифмы членов ГП образуют АП. Толкьо что выше мы показали, 
        // что если нет никаких других условий, то любая последовательность чисел 
        // будет частью АП, а значит и наша последовательность логарифмов может быть частью АП, 
        // следовательно изначальная последовательность может быть ГП. 
        // Вычисление знаменателя этой ГП, уже второй вопрос.

        // Итак, считаем, что члены последовательности - соседние. 
        // Ограничим числа целыми
        // На этапе ввода последовательности потребуем ввести минимум 3 числа
        // Одно число может быть часть чего угодно, два числа - это всегда и АП, и ГП

        // 1. АП - будем сравнивать разность всех соседних чисел. Если она всегда одинакова
        // тогда числа образуют арифметическую последовательность

        // 2. ГП - будем сравнивать частное от деления текущего эл-та на предыдущий 
        // Если частное для всех пар одинаковое - тогда это геометрическая прогрессия

        /// <summary>
        /// Определяет являются ли переданные целые числа
        /// элементами арифметической или геометрической прогрессии.
        /// Возвращает строку со словами "арифметическая", "геометрическая" или "не является"
        /// </summary>
        /// <param name="con">Последовательность любого количества целых чисел или массив</param>
        /// <returns></returns>
        string IsProgression(params int[] sequence)
        {
            bool isArithmetic = true, isGeometric = false;

            int reminder = sequence[1] - sequence[0];
            for (int i = 2; i < sequence.Length; i++)
                if (reminder != (sequence[i] - sequence[i - 1]))
                {
                    isArithmetic = false;
                    break;
                }

            // Ни один член ГП не должен быть равен 0
            if (sequence[0] != 0)
            {
                double quotient = sequence[1] / sequence[0];
                isGeometric = true;
                for (int i = 2; i < sequence.Length; i++)
                {
                    if (sequence[i-1] != 0)
                    {
                        if (quotient != (sequence[i] / sequence[i - 1]))
                        {
                            isGeometric = false;
                            break;
                        }
                    }
                    else
                    {
                        isGeometric = false;
                        break;
                    }
                }
            }
            // переходим сюда, if (sequence[0] != 0)

            return $"{(isArithmetic ? "" : "не ")}" + "арифметическая" +
                   $"{ ((isArithmetic) ? (isGeometric ? " и " : ", но не ") : (isGeometric ? ", но " : ", и не "))}" + 
                   "геометрическая прогрессия.";
        }

        // *Задание 5
        // Функция Аккермана 
        // Здесь https://ru.wikipedia.org/wiki/Функция_Аккермана написано, 
        // что функция A(N, M) начинает стремительно расти при N >= 4
        // При N от 0 до 2 - результат умещается в int, если M умещается в int
        // т.е. линейная зависимость.
        // Зкспоненциальная зависимость начинается с N == 3. 
        // В этом случае переполнение наступит при М == 29, т.к. в этом случае 
        // значение функции Аккермана равно 2^(М+3) - 3 == 2^(29+3) - 3 == 2^32 - 3
        // Если A(N, M) возвращает int, в котором 31 разряд отведён под значение и ещё один под знак,
        // то при М == 29 значение будет занимать уже 32 разряда, что больше int

        // но в реальности прерывание Stack Overflow  появилось уже при М == 11 :((
        // Поэтому ограничим исходные параметры N от 0 до 3, и М от 0 до 10.

        /// <summary>
        /// Создаёт окно меню для ввода параметров для вызова метода FindShortestWord
        /// </summary>
        /// <param name="window">Окно, в котором будет осуществлён ввод данных</param>
        public void MenuAkkerman(WindowOutput window)
        {
            window.newWindow(winWidth, winHeight);
            window.HeaderCenter("ФУНКЦИЯ АККЕРМАНА", winWidth, 2, ConsoleColor.Yellow);
            window.HeaderCenter("A ( N, M)", winWidth, 3, ConsoleColor.Yellow);
            window.HeaderCenter("Так как эта функция растёт гигантскими шагами,", winWidth, 5, ConsoleColor.White);
            window.HeaderCenter("то диапазон вводимых параметров ограничен.", winWidth, 6, ConsoleColor.White);

            Console.CursorVisible = true;
            int a, b, akk;
            a = simpleParser.AskAndParse("Введите первый аргумен N", "Введите число", 0, 3,
                                   10, 8);
            b = simpleParser.AskAndParse("Введите второй аргумент M", "Введите число", 0, 10,
                                   10, 9);
            akk = Akkerman(a, b);

            Console.CursorVisible = false;
            window.HeaderCenter($"Результат функции Аккермана А ({a}, {b}) = {akk}", winWidth, Console.CursorTop + 2, ConsoleColor.White);

            window.HeaderCenter("НАЖМИТЕ ЛЮБУЮ КЛАВИШУ", winWidth, Console.CursorTop + 2, ConsoleColor.DarkYellow);
            Console.ReadKey();
        }

        /// <summary>
        /// Вычисляет функцию Аккермана, используя рекурсию,
        /// A(2, 5), A(1, 2)
        /// A(N, M) = M + 1, если N = 0,
        ///         = A(N - 1, 1), если N > 0, m = 0,
        ///         = A(N - 1, A(N, M - 1)), если N> 0, M > 0.
        /// </summary>
        /// <param name="n">Целое число большее или равное 0</param>
        /// <param name="m">Целое число большее или равное 0</param>
        /// <returns></returns>
        int Akkerman(int n, int m)
        {
            if (n == 0) return m + 1;
            if ((n > 0) & (m == 0)) return Akkerman(n - 1, 1);
            if ((n > 0) & (m > 0)) return Akkerman(n - 1, Akkerman(n, m - 1));

            return 0;
            // пришлось вставить return 0, т.к компилятор ругался, что не все ветки возвращают значение
            // хотя в реальности до сюда дойдёт, если хотя бы одно введённое число < 0,
            // что запрещено постановкой задачи

        }   // int Akkerman(int n, int m)  

    }   // class Mathematical
}   // namespace Homework_Theme_05
