using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Homework_Theme_05
{

    class Program
    {
        static void Main(string[] args)
        {
            #region  Объявление переменных интерфейса

            int winHeight = 20;             // Высота экрана (для меню) 20 строк
            int winWidth = 80;              // Ширина экрана (для меню) 80 строк
            int currItem = 1;               // текущая позиция меню
            ConsoleKey action;              // Переменная, в которую будет считываться нажатие клавиши
            Console.CursorVisible = false;  // Делаем курсор невидимым

            // Это надо, чтобы корректно вводились и выводились русские буквы на Маке
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            MenuItem[] menuItems =          // Пункты меню для вывода на экран
                       {new MenuItem {itemKey = ConsoleKey.D1,    itemName = "1.   МАТРИЦЫ" },
                        new MenuItem {itemKey = ConsoleKey.D2,    itemName = "2.   САМОЕ КОРОТКОЕ СЛОВО" },
                        new MenuItem {itemKey = ConsoleKey.D3,    itemName = "3.   ВСЕ САМЫЕ ДЛИННЫЕ СЛОВА" },
                        new MenuItem {itemKey = ConsoleKey.D4,    itemName = "4.   УДАЛИТЬ ПОВТОРЯЮЩИЕСЯ БУКВЫ" },
                        new MenuItem {itemKey = ConsoleKey.D5,    itemName = "5.   ПРОГРЕССИЯ ЛИ ЭТО" },
                        new MenuItem {itemKey = ConsoleKey.D6,    itemName = "6.   АККЕРМАН" },
                        new MenuItem {itemKey = ConsoleKey.Escape,itemName = "ESC  ВЫХОД" } };

            WindowOutput       window = new WindowOutput();
            #endregion  Объявление переменных интерфейса

            Matrix          matrix = new Matrix();
            StringManipulations sm = new StringManipulations();
            Mathematical      math = new Mathematical();

            do                  // Считываем нажатия, пока не будет ESC
            {
                window.newWindow(winWidth, winHeight);
                window.HeaderCenter("Домашняя работа  №5", winWidth, 2, ConsoleColor.Yellow);
                window.HeaderCenter("Дмитрий Мельников", winWidth, 3, ConsoleColor.Yellow);

                action = window.MenuSelect(menuItems, currItem, winWidth, 5);

                switch (action)
                {
                    case ConsoleKey.D1:
                        matrix.MatrixMenu();
                        currItem = 1;
                        break;

                    case ConsoleKey.D2:
                        sm.MenuShortestWord(window);
                        currItem = 2;
                        break;

                    case ConsoleKey.D3:
                        sm.MenuLongestWords(window);
                        currItem = 3;
                        break;

                    case ConsoleKey.D4:
                        sm.MenuRefineString(window);
                        currItem = 4;
                        break;

                    case ConsoleKey.D5:
                        math.MenuIsProgression(window);
                        currItem = 5;
                        break;

                    case ConsoleKey.D6:
                        math.MenuAkkerman(window);
                        currItem = 6;
                        break;

                    case ConsoleKey.Escape:
                        break;

                    default:
                        break;   // нажата любая другая клавиша - ничего не происходит
                }

            } while (action != ConsoleKey.Escape);


        }   // void Main ();
    }   //  Class Program
}   // namespace Homework_Theme_05

