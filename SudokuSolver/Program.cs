using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo mode;
            Sudoku sudoku = null;
            bool checkDiagonals = false;
            bool centerMustBeMagicSquare = false;
            bool knightsMoveDifferent = false;
            for (; ; )
            { 
                do
                {
                    do
                    {
                        Console.WriteLine("Manual input (M), file read (R), toggle diagonal check (D), toggle center magic square (C) or quit (Q)?");
                        mode = Console.ReadKey();
                    }
                    while (mode.Key != ConsoleKey.M && mode.Key != ConsoleKey.R && mode.Key != ConsoleKey.Q && mode.Key != ConsoleKey.D && mode.Key != ConsoleKey.C && mode.Key != ConsoleKey.K);

                    if (mode.Key == ConsoleKey.Q)
                    {
                        return;
                    }

                    if (mode.Key == ConsoleKey.D)
                    {
                        checkDiagonals = !checkDiagonals;
                        Console.WriteLine($"Diagonal check: {(checkDiagonals ? "ON" : "OFF")}");
                    }
                    else if (mode.Key == ConsoleKey.C)
                    {
                        centerMustBeMagicSquare = !centerMustBeMagicSquare;
                        Console.WriteLine($"Center must be magic square: {(centerMustBeMagicSquare ? "ON" : "OFF")}");
                    }
                    else if (mode.Key == ConsoleKey.K)
                    {
                        knightsMoveDifferent = !knightsMoveDifferent;
                        Console.WriteLine($"Cells knights move apart must be different: {(knightsMoveDifferent ? "ON" : "OFF")}");
                    }
                    else if (mode.Key == ConsoleKey.M)
                    {
                        sudoku = ManualInput();
                    }
                    else if (mode.Key == ConsoleKey.R)
                    {
                        sudoku = FileRead();
                    }
                }
                while (sudoku == null);

                Console.WriteLine();

                sudoku.CheckDiagonals = checkDiagonals;
                sudoku.CenterMustBeMagicSquare = centerMustBeMagicSquare;
                sudoku.KnightsMoveDifferent = knightsMoveDifferent;
                sudoku.Solve();
            }
        }

        static Sudoku FileRead()
        {
            Console.WriteLine("Enter file name");
            var fileName = Console.ReadLine();
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File not found!");
                return null;
            }

            var sudoku = new Sudoku();
            var lines = File.ReadAllLines(fileName);
            var i = 0;
            foreach (var line in lines)
            {
                var position = i * 9;
                foreach (var c in line)
                {
                    if (char.IsDigit(c))
                    {
                        sudoku.SetValue(position, c - '0');
                    }
                    position++;
                    if (position % 9 == 0)
                    {
                        break;
                    }
                }
                i++;
            }

            return sudoku;
        }

        static Sudoku ManualInput()
        {
            Console.WriteLine("Input sudoku line by line. 9 lines in total. End each line with a CR.");

            var sudoku = new Sudoku();
            for (int i = 0; i < 9; ++i)
            {
                var line = Console.ReadLine();

                var position = i*9;
                foreach (var c in line)
                {
                    if (char.IsDigit(c))
                    {
                        sudoku.SetValue(position, c-'0');
                    }
                    position++;
                    if (position % 9 == 0)
                    {
                        break;
                    }
                }
            }
            return sudoku;
        }
    }
}
