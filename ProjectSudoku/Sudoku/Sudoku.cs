using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSudoku
{
    public interface SudokuGeneratorStrategy
    {
        int[,] GenerateGrid();
    }
    class SudokuGenerator : SudokuGeneratorStrategy
    {
        private int BoardSize = 9;
        private const int SubgridSize = 3;
        private const int MaxIterations = 100;
        private int difficult;
        Random random = new Random();
        private int[,] board;

        public SudokuGenerator()
        {
            board = new int[BoardSize, BoardSize];
        }
        public SudokuGenerator(int sudokuDifficult)
        {
            difficult = sudokuDifficult;
            board = new int[BoardSize, BoardSize];
        }

        public int[,] GenerateGrid()
        {
            if (difficult == 1)
            {
                FillDiagonal();
                FillRemaining(0, BoardSize);
            }
            if (difficult == 2)
            {
                FillDiagonal();
            }
            return board;
        }

        private void FillDiagonal()
        {
            for (int i = 0; i < BoardSize; i += SubgridSize)
            {
                FillSubgrid(i, i);
            }
        }

        private void FillSubgrid(int row, int col)
        {
            int[] values = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Shuffle(values);

            for (int i = 0; i < SubgridSize; i++)
            {
                for (int j = 0; j < SubgridSize; j++)
                {
                    board[row + i, col + j] = values[i * SubgridSize + j];
                }
            }
        }

        private void FillRemaining(int row, int col)
        {
            if (col >= BoardSize && row < BoardSize - 1)
            {
                row++;
                col = 0;
            }

            if (row >= BoardSize && col >= BoardSize)
            {
                return;
            }

            if (row < SubgridSize)
            {
                if (col < SubgridSize)
                {
                    col = SubgridSize;
                }
            }
            else if (row < BoardSize - SubgridSize)
            {
                if (col == (int)(row / SubgridSize) * SubgridSize)
                {
                    col += SubgridSize;
                }
            }
            else
            {
                if (col == BoardSize - SubgridSize)
                {
                    row++;
                    col = 0;
                    if (row >= BoardSize)
                    {
                        return;
                    }
                }
            }
            for (int value = 1; value <= BoardSize; value++)
            {
                if (IsValid(row, col, value))
                {
                    board[row, col] = value;
                    if (col < BoardSize - 1)
                    {
                        FillRemaining(row, col + 1);
                    }
                    else
                    {
                        FillRemaining(row + 1, 0);
                    }

                    if (row >= BoardSize || col >= BoardSize)
                    {
                        return;
                    }
                }
            }
        }

        private bool IsValid(int row, int col, int value)
        {
            for (int i = 0; i < BoardSize; i++)
            {
                if (board[row, i] == value || board[i, col] == value)
                {
                    return false;
                }
            }

            int subgridRow = (row / SubgridSize) * SubgridSize;
            int subgridCol = (col / SubgridSize) * SubgridSize;

            for (int i = 0; i < SubgridSize; i++)
            {
                for (int j = 0; j < SubgridSize; j++)
                {
                    if (board[subgridRow + i, subgridCol + j] == value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private void Shuffle<T>(T[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                T temp = array[j];
                array[j] = array[i];
                array[i] = temp;
            }
        }
        public void PrintBoard()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }

    public class Sudoku
    {
        private int[,] grid;
        public Sudoku(SudokuGeneratorStrategy strategy)
        {
            do
            {
                grid = strategy.GenerateGrid();
                Console.WriteLine(grid.GetLength(0));
            } while (IsSolved());
        }
        public void Play()
        {
            int row = 0;
            int col = 0;
            bool enter, exit;
            int value = 0;
            ConsoleKeyInfo input;
            while (!IsSolved())
            {
                enter = false;
                exit = false;
                do
                {
                    Console.SetCursorPosition(row, col);
                    Console.ForegroundColor = ConsoleColor.White;
                    DisplayGrid(row, col);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n\t\tНатиснiть на кнопки (1-{0}), (0 стерти число, ESC вихiд) ", grid.GetLength(0));
                    input = Console.ReadKey(true);
                    Console.ForegroundColor = ConsoleColor.Green;
                    switch (input.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (row > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                row--;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (row < grid.GetLength(0) - 1)
                            {
                                row++;
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (col > 0)
                            {
                                col--;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if (col < grid.GetLength(1) - 1)
                            {
                                col++;
                            }
                            break;
                        case ConsoleKey.NumPad0:
                        case ConsoleKey.D0:
                            value = 0;
                            enter = true;
                            break;
                        case ConsoleKey.NumPad1:
                        case ConsoleKey.D1:
                            value = 1;
                            enter = true;
                            break;
                        case ConsoleKey.NumPad2:
                        case ConsoleKey.D2:
                            value = 2;
                            enter = true;
                            break;
                        case ConsoleKey.NumPad3:
                        case ConsoleKey.D3:
                            value = 3;
                            enter = true;
                            break;
                        case ConsoleKey.NumPad4:
                        case ConsoleKey.D4:
                            value = 4;
                            enter = true;
                            break;
                        case ConsoleKey.NumPad5:
                        case ConsoleKey.D5:
                            value = 5;
                            enter = true;
                            break;
                        case ConsoleKey.NumPad6:
                        case ConsoleKey.D6:
                            value = 6;
                            enter = true;
                            break;
                        case ConsoleKey.NumPad7:
                        case ConsoleKey.D7:
                            value = 7;
                            enter = true;
                            break;
                        case ConsoleKey.NumPad8:
                        case ConsoleKey.D8:
                            value = 8;
                            enter = true;
                            break;
                        case ConsoleKey.NumPad9:
                        case ConsoleKey.D9:
                            value = 9;
                            enter = true;
                            break;

                        case ConsoleKey.Escape:
                            enter = true;
                            exit = true;
                            break;
                    }
                } while (enter != true);
                Console.ForegroundColor = ConsoleColor.White;
                if (exit == true)
                {
                    Console.WriteLine("\t\tВи здалися, прозводимо вихiд....");
                    Console.ReadKey();
                    break;
                }
                if (CanPlaceNumber(row, col, value))
                {
                    if (value == 0)
                        Console.WriteLine("\t\tКлiтинка ({0}, {1}) успiшно стерта ", row + 1, col + 1);
                    else
                        Console.WriteLine("\t\tЧисло {0} успiшно записано в клiтинку ({1}, {2})...", value, row + 1, col + 1);
                    grid[row, col] = value;
                }
                else
                {
                    if (value == 0)
                        Console.WriteLine("\t\tНе можливо стерти клiтинку ({0}, {1})...", row + 1, col + 1);
                    else
                        Console.WriteLine("\t\tНе можливо записати число {0} у клiтинку ({1}, {2})...", value, row + 1, col + 1);
                }
                Console.ReadKey();
            }
        }
        private bool IsSolved()
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(0); col++)
                {
                    if (grid[row, col] == 0)
                    {
                        return false;
                    }
                }
            }
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                if (!IsValidRow(i))
                {
                    return false;
                }
                if (!IsValidColumn(i))
                {
                    return false;
                }
                if (grid.GetLength(0) == 9)
                {
                    if (!IsValidSquare(i / 3 * 3, i % 3 * 3))
                    {
                        return false;
                    }
                }
            }
            DisplayGrid(1, 1);
            Console.WriteLine("\t\tВiтаємо, ви вирiшили Судоку!");
            Console.ReadKey(true);
            return true;
        }

        private bool IsValidRow(int row)
        {
            bool[] usedValues = new bool[grid.GetLength(0)];

            for (int col = 0; col < grid.GetLength(0); col++)
            {
                int value = grid[row, col];

                if (value != 0)
                {
                    if (usedValues[value - 1])
                    {
                        return false;
                    }

                    usedValues[value - 1] = true;
                }
            }
            return true;
        }

        private bool IsValidColumn(int col)
        {
            bool[] usedValues = new bool[grid.GetLength(0)];

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                int value = grid[row, col];
                if (value != 0)
                {
                    if (usedValues[value - 1])
                    {
                        return false;
                    }
                    usedValues[value - 1] = true;
                }
            }
            return true;
        }

        private bool IsValidSquare(int startRow, int startCol)
        {
            bool[] usedValues = new bool[9];

            for (int row = startRow; row < startRow + 3; row++)
            {
                for (int col = startCol; col < startCol + 3; col++)
                {
                    int value = grid[row, col];

                    if (value != 0)
                    {
                        if (usedValues[value - 1])
                        {
                            return false;
                        }

                        usedValues[value - 1] = true;
                    }
                }
            }
            return true;
        }

        private bool CanPlaceNumber(int row, int col, int value)
        {
            if (value < 1 || value > grid.GetLength(0))
            {
                if (value == 0)
                {
                    if (grid[row, col] == 0)
                    {
                        return false;
                    }
                    return true;
                }

                return false;
            }
            for (int c = 0; c < grid.GetLength(0); c++)
            {
                if (grid[row, c] == value)
                {
                    return false;
                }
            }
            for (int r = 0; r < grid.GetLength(0); r++)
            {
                if (grid[r, col] == value)
                {
                    return false;
                }
            }

            int squareRow = row / 3 * 3;
            int squareCol = col / 3 * 3;
            for (int r = squareRow; r < squareRow + 3; r++)
            {
                for (int c = squareCol; c < squareCol + 3; c++)
                {
                    if (grid[r, c] == value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void DisplayGrid(int c1, int c2)
        {
            Console.Clear();
            Console.WriteLine("\t\tЛаскаво просимо до гри Судоку!");

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                if (row == 3 || row == 6)
                {
                    Console.Write("\t\t");
                    for (int i = 0; i < grid.GetLength(0) - 1; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("---");
                    }
                    Console.WriteLine();
                }
                Console.Write("\t\t");
                Console.Write(" ");
                for (int col = 0; col < grid.GetLength(0); col++)
                {
                    if (row == c1 && col == c2)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(grid[row, col] == 0 ? "-" : grid[row, col].ToString());
                    Console.Write(" ");
                    if (col == 2 || col == 5)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" | ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
