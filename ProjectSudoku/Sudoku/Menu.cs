using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectSudoku;

namespace MenuSudoku
{
    public static class Menu // ЦЕ МЕНЮ
    {
        public static void MenuSud()
        {
            bool exit = false;
            int selectedMenuItem = 0;
            Console.Clear();
            Console.CursorVisible = false;
            while (exit != true)
            {
                Sudokuprint();
                Console.ForegroundColor = (selectedMenuItem == 0) ? ConsoleColor.Green : ConsoleColor.White;
                Console.WriteLine("\n\n\t\t\tГрати");
                Console.ForegroundColor = (selectedMenuItem == 1) ? ConsoleColor.Green : ConsoleColor.White;
                Console.WriteLine("\t\t\tВихiд");
                Console.ForegroundColor = ConsoleColor.White;
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedMenuItem = Math.Max(0, selectedMenuItem - 1);
                        break;

                    case ConsoleKey.DownArrow:
                        selectedMenuItem = Math.Min(1, selectedMenuItem + 1);
                        break;

                    case ConsoleKey.Enter:
                        switch (selectedMenuItem)
                        {
                            case 0:
                                SudokuEnter();
                                break;

                            case 1:
                                Console.Clear();
                                Console.WriteLine("\n\n\t\t\tДо побачення!");
                                Console.ForegroundColor = ConsoleColor.Black;
                                exit = true;
                                break;
                        }
                        break;
                }
            }
        }

        public static void Sudokuprint() // ВИВОДИМО ГАРНИЙ НАДПИС
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\t  SSS   U   U  DDDD    OOO   K   K  U   U");
            Console.WriteLine("\t S      U   U  D   D  O   O  K  K   U   U");
            Console.WriteLine("\t  SSS   U   U  D   D  O   O  KKK    U   U");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t     S  U   U  D   D  O   O  K  K   U   U");
            Console.WriteLine("\t  SSS    UUU   DDDD    OOO   K   K   UUU");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void SudokuEnter() // ВИБИРАЄМО ВАЖКІСТЬ СУДОКУ
        {
            bool exitmenu = false;
            int selectedMenuItem = 0;
            while (exitmenu != true)
            {
                Sudokuprint();
                Console.ForegroundColor = (selectedMenuItem == 0) ? ConsoleColor.Green : ConsoleColor.White;
                Console.WriteLine("\n\n\t\t\tЛегкий");
                Console.ForegroundColor = (selectedMenuItem == 1) ? ConsoleColor.Green : ConsoleColor.White;
                Console.WriteLine("\t\t\tВажкий");
                Console.ForegroundColor = (selectedMenuItem == 2) ? ConsoleColor.Green : ConsoleColor.White;
                Console.WriteLine("\t\t     Вихiд до меню");
                Console.ForegroundColor = ConsoleColor.White;
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedMenuItem = Math.Max(0, selectedMenuItem - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        selectedMenuItem = Math.Min(2, selectedMenuItem + 1);
                        break;
                    case ConsoleKey.Enter:
                        exitmenu = true;
                        switch (selectedMenuItem) // Вибираємо важкість саме тут
                        {
                            case 0:
                                SudokuGeneratorStrategy strategyEasy = new SudokuGenerator(1);
                                Sudoku sudokuEasy = new Sudoku(strategyEasy);
                                sudokuEasy.Play();
                                break;

                            case 1:
                                SudokuGeneratorStrategy strategyHard = new SudokuGenerator(2);
                                Sudoku sudokuHard = new Sudoku(strategyHard);
                                sudokuHard.Play();
                                break;
                            case 2:
                                Console.WriteLine("\t\tПрозводимо вихiд до меню...");
                                Console.ReadKey(true);
                                break;
                        }
                        break;
                }
            }
        }
    }
}
