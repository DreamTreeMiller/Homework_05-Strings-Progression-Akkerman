using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_Theme_05
{
    // Требуется написать несколько методов
    //
    // Задание 1.
    // Воспользовавшись решением задания 3 четвертого модуля
    // 1.1. Создать метод, принимающий число и матрицу, возвращающий матрицу умноженную на число
    // 1.2. Создать метод, принимающий две матрицу, возвращающий их сумму
    // 1.3. Создать метод, принимающий две матрицу, возвращающий их произведение
    //

    class Matrix
    {

        #region объявления переменных

        int winHeight = 20;             // Высота экрана (для меню) 40 строк
        int winWidth = 80;              // Ширина экрана (для меню) 80 строк
        Random r;

        static MenuItem[] menuItems =          // Пункты меню для вывода на экран
                   {new MenuItem {itemKey = ConsoleKey.D1,    itemName = "1.   УМНОЖЕНИЕ МАТРИЦЫ НА ЧИСЛО" },
                    new MenuItem {itemKey = ConsoleKey.D2,    itemName = "2.   СЛОЖЕНИЕ МАТРИЦ" },
                    new MenuItem {itemKey = ConsoleKey.D3,    itemName = "3.   ВЫЧИТАНИЕ МАТРИЦ" },
                    new MenuItem {itemKey = ConsoleKey.D4,    itemName = "4.   УМНОЖЕНИЕ МАТРИЦ" },
                    new MenuItem {itemKey = ConsoleKey.Escape,itemName = "ESC  ВЫХОД" } };

        // Окно, в котором будем выводить меню матрицы
        WindowOutput windowM;
        SimpleParser simpleParser;

        #endregion объявления переменных

        public Matrix()
        {
            windowM      = new WindowOutput();
            simpleParser = new SimpleParser();
                       r = new Random();
        }

        public void MatrixMenu()
        {
            ConsoleKey action;       // Переменная, в которую будет считываться нажатие клавиши
            int currItem = 1;        // Текущий пункт меню


            do                  // Считываем нажатия, пока не будет ESC
            {
                windowM.newWindow(winWidth, winHeight);
                Console.CursorVisible = false;  // Делаем курсор невидимым
                windowM.HeaderCenter("ОПЕРАЦИИ С МАТРИЦАМИ", winWidth, 2, ConsoleColor.Yellow);
                action = windowM.MenuSelect(menuItems, currItem, winWidth, 4);

                switch (action)
                {
                    case ConsoleKey.D1:
                        MenuMultiplyMatrixByNumber();
                        currItem = 1;
                        break;

                    case ConsoleKey.D2:
                        MenuAddMatrixes();
                        currItem = 2;
                        break;

                    case ConsoleKey.D3:
                        MenuSubtractMatrixes();
                        currItem = 3;
                        break;

                    case ConsoleKey.D4:
                        MenuMultiplyMatrixes();
                        currItem = 4;
                        break;

                    case ConsoleKey.Escape:
                        Console.WriteLine("ДО СВИДАНИЯ!");
                        break;

                    default:
                        break;   // нажата любая другая клавиша - ничего не происходит
                }

            } while (action != ConsoleKey.Escape);

        }   // public void matrixMenu()

        /// <summary>
        /// Заполняет ячейки матрицы случайными числами из диапаона
        /// </summary>
        /// <param name="m">Количество строк матрицы</param>
        /// <param name="n">Количество столбцов матрицы</param>
        /// <param name="low">Нижняя включаемая граница случайных чисел</param>
        /// <param name="up">Верхняя включаемая граница случайных чисел</param>
        /// <returns></returns>
        int[,] RandomMatrix(int m, int n, int low, int up)
        {
            int[,] matrix = new int[m, n];
            
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    matrix[i, j] = r.Next(low, up + 1);

            return matrix;
        }

        int[,] MultiplyMatrixByNumber(int num, int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i, j] *= num;
            return matrix;
        }

        /// <summary>
        /// Печатает матрицу в заданной области буфера экрана с заданной шириной ячейки. 
        /// Расстояние между ячейками - пробел. Выравнивание в ячейке по правому краю
        /// </summary>
        /// <param name="matrixToPrint">Матрица для вывода.</param>
        /// <param name="cellSize">Количество символов для вывода каждой ячейки матрицы</param>
        /// <param name="topLeftX">Столбец, с которого выводить матрицу</param>
        /// <param name="topLeftY">Строка, с которой выводить матрицу</param>
        void PrintMatrix(int[,] matrixToPrint, int cellSize, int topLeftX, int topLeftY)
        {
            int i, j;

            for (i = 0; i < matrixToPrint.GetLength(0); i++)
            {
                Console.SetCursorPosition(topLeftX, topLeftY + i);
                Console.Write("| ");
                for (j = 0; j < matrixToPrint.GetLength(1); j++)
                {
                    Console.SetCursorPosition(topLeftX + 2 + j * (1 + cellSize), topLeftY + i);
                    Console.Write($"{matrixToPrint[i, j]}".PadLeft(cellSize));
                }
                Console.SetCursorPosition(topLeftX + 2 + j * (1 + cellSize), topLeftY + i);
                Console.Write(" |");

            }
        }

        /// <summary>
        /// Метод ввода параметров для умножения матрицы на число. Вызывает метод умножения матрицы. 
        /// Выводит число, исходную и умноженную матрицы на печать
        /// </summary>
        public void MenuMultiplyMatrixByNumber()
        {
            int winHeight = 30;        // Высота экрана (для меню) 40 строк
            int winWidth = 120;         // Ширина экрана (для меню) 80 строк
            bool cursorVisibility;


            int num;             // вводимое число 
            int m, n;            // размерности матрицы : m - число строк (1 х 10), n - число столбцов (1 х 10)

            windowM.newWindow(winWidth, winHeight);
            Console.SetBufferSize(200, 40);
            windowM.HeaderCenter("УМНОЖЕНИЕ МАТРИЦЫ НА ЧИСЛО", winWidth, 2, ConsoleColor.Yellow);
            Console.WriteLine();
            cursorVisibility = Console.CursorVisible;
            Console.CursorVisible = true;

            m = simpleParser.AskAndParse("Введите количество строк матрицы", "Введите число", 1, 10, 0, 4);
            n = simpleParser.AskAndParse("Введите количество столбцов матрицы", "Введите число", 1, 10, 0, 5);
            num = simpleParser.AskAndParse("Введите число, на которое хотите умножить матрицу",
                                     "Введите число", -100, 100, 0, 6);

            Console.CursorVisible = cursorVisibility;

            // Создаём матрицу (m, n) и инициализируем случайными числами от -10 до 10 включительно
            int[,] matrix = RandomMatrix(m, n, -10, 10);

            // Выводим изначальную матрицу
            // 3 - ширина поля для вывода каждого элемента матрицы
            // 5 и 8  - координаты Х и У левого верхнего угла матрицы
            PrintMatrix(matrix, 3, 10, 8);

            // Умножаем матрицу на число. Результат храним в той же матрице
            matrix = MultiplyMatrixByNumber(num, matrix);

            Console.SetCursorPosition(2, 8 + m / 2);
            Console.Write($"{num} x".PadLeft(6));

            // Выводим * num =  между матрицами
            Console.SetCursorPosition(10 + 1 + n * 4 + 2 + 5, 8 + m / 2);
            Console.Write("=");

            // Выводим получившуюся матрицу
            // 5 - ширина поля для вывода каждого элемента матрицы
            // 5+1+n*4+2+20 и 8  - координаты Х и У левого верхнего угла матрицы
            PrintMatrix(matrix, 5, 10 + 1 + n * 4 + 2 + 9, 8);

            windowM.HeaderCenter("НАЖМИТЕ ЛЮБУЮ КЛАВИШУ", winWidth, Console.CursorTop + 2, ConsoleColor.DarkYellow);
            Console.ReadKey();
        }

        /// <summary>
        /// Складывает две матрицы. 
        /// Проверка равенства кол-ва строк и столбцов в исходных матрицах не делается.
        /// Всё на совести разработчика
        /// </summary>
        /// <param name="matrixA">Первая матрица с m строк и n столбцов</param>
        /// <param name="matrixB">Вторая матрица с m строк и n столбцов</param>
        /// <returns></returns>
        int[,] AddMatrixes(int[,] matrixA, int[,] matrixB)
        {
            int m = matrixA.GetLength(0);
            int n = matrixA.GetLength(1);
            int[,] matrixC = new int[m, n];

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {
                    matrixC[i, j] = matrixA[i, j] + matrixB[i, j];
                }
            return matrixC;
        }

        /// <summary>
        /// Метод ввода параметров для сложения матриц. Вызывает метод сложения матриц. 
        /// Выводит исходные матрицы и результат их сложения на печать
        /// </summary>
        public void MenuAddMatrixes()
        {
            int winHeight = 30;        // Высота экрана (для меню) 40 строк
            int winWidth = 120;         // Ширина экрана (для меню) 80 строк
            bool cursorVisibility;


            int m, n;            // размерности матрицы : m - число строк (1 х 10), n - число столбцов (1 х 10)

            windowM.newWindow(winWidth, winHeight);
            Console.SetBufferSize(200, 40);
            windowM.HeaderCenter("СЛОЖЕНИЕ МАТРИЦ", winWidth, 2, ConsoleColor.Yellow);
            cursorVisibility = Console.CursorVisible;
            Console.CursorVisible = true;

            m = simpleParser.AskAndParse("Введите количество строк матриц", "Введите число", 1, 10, 0, 4);
            n = simpleParser.AskAndParse("Введите количество столбцов матриц", "Введите число", 1, 10, 0, 5);

            Console.CursorVisible = cursorVisibility;

            // Создаём матрицы, заполняем случайными числами и складываем
            int[,] matrixA = RandomMatrix(m, n, -10, 10);      // создаём матрицу A m, n - первое слагаемое
            int[,] matrixB = RandomMatrix(m, n, -10, 10);      // создаём матрицу B m, n - второе слагаемое
            int[,] matrixC = AddMatrixes(matrixA, matrixB);    // создаём матрицу C m, n - сумма

            // Выводим матрицу A
            // 3 - ширина поля для вывода каждого элемента матрицы
            // 5 и 8  - координаты Х и У левого верхнего угла матрицы
            PrintMatrix(matrixA, 3, 5, 8);

            // Выводим знак плюс "+"  между матрицами
            Console.SetCursorPosition(5 + 1 + n * 4 + 2 + 4, 8 + m / 2);
            Console.Write("+");

            // Выводим матрицу B
            // 3 - ширина поля для вывода каждого элемента матрицы
            // 5+1+n*4+2+9 и 8  - координаты Х и У левого верхнего угла матрицы
            PrintMatrix(matrixB, 3, 5 + 1 + n * 4 + 2 + 9, 8);

            // Выводим знак равно "="  между матрицами
            Console.SetCursorPosition(5 + (1 + n * 4 + 2 + 9) * 2 - 5, 8 + m / 2);
            Console.Write($"=");

            // Выводим получившуюся матрицу
            // 5 - ширина поля для вывода каждого элемента матрицы
            // 5 + (1 + n * 4 + 2 + 9)*2 и 8  - координаты Х и У левого верхнего угла матрицы
            PrintMatrix(matrixC, 3, 5 + (1 + n * 4 + 2 + 9) * 2, 8);

            windowM.HeaderCenter("НАЖМИТЕ ЛЮБУЮ КЛАВИШУ", winWidth, Console.CursorTop + 2, ConsoleColor.DarkYellow);
            Console.ReadKey();

        }

        /// <summary>
        /// Вычитает из матрицы A матрицу B. 
        /// Проверка равенства кол-ва строк и столбцов в исходных матрицах не делается.
        /// Всё на совести разработчика
        /// </summary>
        /// <param name="matrixA">Первая матрица с m строк и n столбцов</param>
        /// <param name="matrixB">Вторая матрица с m строк и n столбцов</param>
        /// <returns></returns>
        int[,] SubtractMatrixes(int[,] matrixA, int[,] matrixB)
        {
            int m = matrixA.GetLength(0);
            int n = matrixA.GetLength(1);
            int[,] matrixC = new int[m, n];

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {
                    matrixC[i, j] = matrixA[i, j] - matrixB[i, j];
                }
            return matrixC;
        }

        /// <summary>
        /// Метод ввода параметров для вычитания матриц. Вызывает метод вычитания матриц. 
        /// Выводит исходные матрицы и результат их вычитания на печать
        /// </summary>
        public void MenuSubtractMatrixes()
        {
            int winHeight = 30;        // Высота экрана (для меню) 40 строк
            int winWidth = 120;         // Ширина экрана (для меню) 80 строк
            bool cursorVisibility;

            int m, n;            // размерности матрицы : m - число строк (1 х 10), n - число столбцов (1 х 10)

            windowM.newWindow(winWidth, winHeight);
            Console.SetBufferSize(200, 40);
            windowM.HeaderCenter("ВЫЧИТАНИЕ МАТРИЦ", winWidth, 2, ConsoleColor.Yellow);
            cursorVisibility = Console.CursorVisible;
            Console.CursorVisible = true;

            m = simpleParser.AskAndParse("Введите количество строк матриц", "Введите число", 1, 10, 0, 4);
            n = simpleParser.AskAndParse("Введите количество столбцов матриц", "Введите число", 1, 10, 0, 5);

            Console.CursorVisible = cursorVisibility;

            // Создаём матрицы, заполняем случайными числами и вычитаем из матрицы А матрицу В
            int[,] matrixA = RandomMatrix(m, n, -10, 10);         // создаём матрицу A m, n - уменьшаемое
            int[,] matrixB = RandomMatrix(m, n, -10, 10);         // создаём матрицу B m, n - вычитаемое
            int[,] matrixC = SubtractMatrixes(matrixA, matrixB);  // создаём матрицу C m, n - разность

            // Выводим матрицу A
            // 3 - ширина поля для вывода каждого элемента матрицы
            // 5 и 8  - координаты Х и У левого верхнего угла матрицы
            PrintMatrix(matrixA, 3, 5, 8);

            // Выводим знак минус "-"  между матрицами
            Console.SetCursorPosition(5 + 1 + n * 4 + 2 + 4, 8 + m / 2);
            Console.Write("-");

            // Выводим матрицу B
            // 5 - ширина поля для вывода каждого элемента матрицы
            // 5+1+n*4+2+9 и 8  - координаты Х и У левого верхнего угла матрицы
            PrintMatrix(matrixB, 3, 5 + 1 + n * 4 + 2 + 9, 8);

            // Выводим знак равно "-"  между матрицами
            Console.SetCursorPosition(5 + (1 + n * 4 + 2 + 9) * 2 - 5, 8 + m / 2);
            Console.Write($"=");

            // Выводим получившуюся матрицу
            // 5 - ширина поля для вывода каждого элемента матрицы
            // 5 + (1 + n * 4 + 2 + 9) * 2 и 8  - координаты Х и У левого верхнего угла матрицы
            PrintMatrix(matrixC, 3, 5 + (1 + n * 4 + 2 + 9) * 2, 8);

            windowM.HeaderCenter("НАЖМИТЕ ЛЮБУЮ КЛАВИШУ", winWidth, Console.CursorTop + 2, ConsoleColor.DarkYellow);
            Console.ReadKey();
        }

        /// <summary>
        /// Умножает матрицу А (m, n) справа на матрицу В (n, k).
        /// Количество столбцов n матрицы А совпадает с количеством строк матрицы В
        /// Проверка соответствия матриц друг другу не осуществляется.
        /// Возвращает результирующую матрицу C (m, k)
        /// </summary>
        /// <param name="matrixA">Матрица слева А (m, n)</param>
        /// <param name="matrixB">Матрица справа В (n, k)</param>
        /// <returns></returns>
        int[,] MultiplyMatrixes(int[,] matrixA, int[,] matrixB)
        {
            int m = matrixA.GetLength(0);        
            int n = matrixA.GetLength(1);
            int k = matrixB.GetLength(1);
            int[,] matrixC = new int[m, k];

            for (int i = 0; i < m; i++)
                for (int j = 0; j < k; j++)
                    for (int e = 0; e < n; e++)
                        matrixC[i, j] += matrixA[i, e] * matrixB[e, j];

            return matrixC;
        }

        /// <summary>
        /// Метод ввода параметров для умножения матриц. Вызывает метод умножения матриц. 
        /// Выводит исходные матрицы и результат их умножения на печать
        /// </summary>
        public void MenuMultiplyMatrixes()
        {
            int winHeight = 30;        // Высота экрана (для меню) 40 строк
            int winWidth = 120;         // Ширина экрана (для меню) 80 строк
            bool cursorVisibility;

            int m, n, k;         // размерности матрицы A: m - число строк (1 х 10), n - число столбцов (1 х 10)
                                 // размерности матрицы B: n - число строк (1 х 10), k - число столбцов (1 х 10)

            windowM.newWindow(winWidth, winHeight);
            Console.SetBufferSize(200, 40);
            windowM.HeaderCenter("УМНОЖЕНИЕ МАТРИЦ", winWidth, 2, ConsoleColor.Yellow);
            Console.WriteLine();
            cursorVisibility = Console.CursorVisible;
            Console.CursorVisible = true;

            m = simpleParser.AskAndParse("Введите количество строк матрицы А", "Введите число", 1, 10, 0, 4);
            n = simpleParser.AskAndParse("Введите количество столбцов матрицы А", "Введите число", 1, 10, 0, 5);
            Console.SetCursorPosition(0, 7);
            Console.Write($"В матрице B {n} строк");
            k = simpleParser.AskAndParse("Введите количество столбцов матрицы B", "Введите число", 1, 10, 0, 8);

            Console.CursorVisible = cursorVisibility;

            // Создаём матрицы, заполняем случайными числами и перемножаем
            int[,] matrixA = RandomMatrix(m, n, -10, 11);         // создаём матрицу A m, n - левый множитель
            int[,] matrixB = RandomMatrix(n, k, -10, 11);         // создаём матрицу B n, k - правый множитель
            int[,] matrixC = MultiplyMatrixes(matrixA, matrixB);  // создаём матрицу C m, k - произведение

            int offsetA = m >= n ? 0 : (n - m) / 2;
            int offsetB = m >= n ? (m - n) / 2 : 0;

            // Выводим матрицу A
            // 3 - ширина поля для вывода каждого элемента матрицы
            // 5 и 8  - координаты Х и У левого верхнего угла матрицы
            PrintMatrix(matrixA, 3, 5, 10 + offsetA);

            // Выводим знак умножить "X"  между матрицами
            Console.SetCursorPosition(5 + 1 + n * 4 + 2 + 3,
                                      10 + (offsetA == 0 ? offsetB + n / 2 : offsetA + m / 2));
            Console.Write("x");

            // Выводим матрицу B
            // 5 - ширина поля для вывода каждого элемента матрицы
            //  5 + 1 + n * 4 + 2 + 5 и 8  - координаты Х и У левого верхнего угла матрицы
            PrintMatrix(matrixB, 3, 5 + 1 + n * 4 + 2 + 6, 10 + offsetB);

            // Выводим знак равно "="  между матрицами
            Console.SetCursorPosition(5 + 1 + n * 4 + 2 + 6 + 1 + k * 4 + 2 + 3,
                                      10 + (offsetA == 0 ? offsetB + n / 2 : offsetA + m / 2));
            Console.Write($"=");

            // Выводим получившуюся матрицу
            // 5 - ширина поля для вывода каждого элемента матрицы
            // 5+1+n*4+2+20 и 8  - координаты Х и У левого верхнего угла матрицы
            PrintMatrix(matrixC, 4, 5 + 1 + n * 4 + 2 + 6 + 1 + k * 4 + 2 + 6, 10 + offsetA);

            windowM.HeaderCenter("НАЖМИТЕ ЛЮБУЮ КЛАВИШУ",
                                 winWidth,
                                 10 + (m > n ? m : n) + 2,
                                 ConsoleColor.DarkYellow);
            Console.ReadKey();
        }


    }   // class Matrix
}   // namespace Homework_Theme_04
