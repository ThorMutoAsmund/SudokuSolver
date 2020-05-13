using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class Sudoku
    {
        private int[] slots = new int[81];

        public bool CheckDiagonals { get; set; } = false;
        public bool CenterMustBeMagicSquare { get; set; } = false;
        public bool KnightsMoveDifferent { get; set; } = false;


        public void SetValue(int position, int value)
        {
            if (position < 0 || position >= 81)
            {
                return;
            }

            this.slots[position] = value;
        }

        public bool IsLegal(int[] slots)
        {
            bool IsUnique(List<int> theList)
            {
                var numbers = theList.Where(a => a != 0).ToList();
                return numbers.Distinct().Count() == numbers.Count();
            }

            // Small squares
            for (int y = 0; y < 3; ++y)
            {
                for (int x = 0; x < 3; ++x)
                {
                    if (!IsUnique(new List<int>() { 
                        slots[y * 27 + x * 3], slots[y * 27 + x * 3 + 1], slots[y * 27 + x * 3 + 2],
                        slots[y * 27 + x * 3 + 9], slots[y * 27 + x * 3 + 10], slots[y * 27 + x * 3 + 11],
                        slots[y * 27 + x * 3 + 18], slots[y * 27 + x * 3 + 19], slots[y * 27 + x * 3 + 20],
                    }))
                    {
                        return false;
                    }
                }
            }

            // Horizontal lines
            for (int y = 0; y < 9; ++y)
            {
                if (!IsUnique(new List<int>() {
                    slots[y * 9], slots[y * 9 + 1],slots[y * 9 + 2],
                    slots[y * 9 + 3], slots[y * 9 + 4], slots[y * 9 + 5],
                    slots[y * 9 + 6], slots[y * 9 + 7], slots[y * 9 + 8]
                }))
                {
                    return false;
                }
            }

            // Vertical lines
            for (int x = 0; x < 9; ++x)
            {
                if (!IsUnique(new List<int>() {
                    slots[x], slots[x + 9], slots[x + 18],
                    slots[x + 27], slots[x + 36], slots[x + 45],
                    slots[x + 54], slots[x + 63], slots[x + 72]
                }))
                {
                    return false;
                }
            }

            // Diagonals
            if (this.CheckDiagonals)
            {
                if (!IsUnique(new List<int>() {
                        slots[0], slots[10], slots[20],
                        slots[30], slots[40], slots[50],
                        slots[60], slots[70], slots[80]
                    }))
                {
                    return false;
                }
                if (!IsUnique(new List<int>() {
                        slots[8], slots[16], slots[24],
                        slots[32], slots[40], slots[48],
                        slots[56], slots[64], slots[72]
                    }))
                {
                    return false;
                }
            }

            if (this.CenterMustBeMagicSquare)
            {
                if (slots[30] + slots[31] + slots[32] != 15 ||
                    slots[39] + slots[40] + slots[41] != 15 ||
                    slots[48] + slots[49] + slots[50] != 15 ||
                    slots[30] + slots[39] + slots[48] != 15 ||
                    slots[31] + slots[40] + slots[49] != 15 ||
                    slots[32] + slots[41] + slots[50] != 15 ||
                    slots[30] + slots[40] + slots[50] != 15 ||
                    slots[32] + slots[40] + slots[48] != 15)
                {
                    return false;
                }
            }

            if (this.KnightsMoveDifferent)
            {
                for (int y = 0; y < 9; ++y)
                {
                    for (int x = 0; x < 9; ++x)
                    {
                        int i = y * 9 + x;
                        if (y)
                    }
                }
            }

            return true;
        }

        public void Solve()
        {
            var solutionFound = false;
            void Recurse(int pos)
            {
                if (IsLegal(this.slots))
                {
                    do
                    {
                        pos++;
                        if (pos == 81)
                        {
                            Display();
                            solutionFound = true;
                            return;
                        }
                    }
                    while (slots[pos] != 0);

                    for (int i = 1; i <= 9; ++i)
                    {
                        this.slots[pos] = i;
                        Recurse(pos);
                    }
                    this.slots[pos] = 0;
                }
            }

            Recurse(-1);
            if (!solutionFound)
            {
                Console.WriteLine("No solutions found");
            }
        }

        public void Display()
        {
            Console.WriteLine("-----------");
            for (int i = 0; i < 81; ++i)
            {
                Console.Write(slots[i] == 0 ? "." : slots[i].ToString());
                if (i % 3 == 2)
                {
                    Console.Write(" ");
                }
                if (i % 9 == 8)
                {
                    Console.WriteLine();
                    if (i % 27 == 26 && i != 80)
                    {
                        Console.WriteLine();
                    }
                }
            }
            Console.WriteLine("-----------");
        }
    }
}
