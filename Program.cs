using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

public class Program
{
    static int Rows = 50, Cols = 26, Width = 10, VRows = 15, VCols = 8;
    static Cell[,] Grid = new Cell[Rows, Cols];
    static int curR = 0, curC = 0, offR = 0, offC = 0;
    static string input = "";
    static bool editing = false;

    public static void Main()
    {
        for (int r = 0; r < Rows; r++)
            for (int c = 0; c < Cols; c++) Grid[r, c] = new Cell();

        while (true)
        {
            Render();
            var key = Console.ReadKey(true);
            if (editing) HandleEdit(key);
            else HandleNav(key);
        }
    }

    static void Render()
    {
        Console.Clear();

        Console.Write(new string(' ', Width) + "|");
        for (int c = 0; c < VCols; c++)
            Console.Write($"{(char)('A' + c + offC)}".PadLeft(Width) + "|");
        
        Console.WriteLine("\n" + new string('-', (VCols + 1) * (Width + 1)));


        for (int r = 0; r < VRows; r++)
        {
            int activeR = r + offR;
            Console.Write($"{activeR + 1}".PadLeft(Width) + "|");
            for (int c = 0; c < VCols; c++)
            {
                int activeC = c + offC;
                var cell = Grid[activeR, activeC];
                bool isCur = activeR == curR && activeC == curC;
                
                if (isCur) Console.BackgroundColor = editing ? ConsoleColor.DarkGreen : ConsoleColor.DarkBlue;
                Console.Write(cell.Display.PadRight(Width).Substring(0, Width));
                Console.ResetColor();
                Console.Write("|");
            }
            Console.WriteLine();
        }

        Console.SetCursorPosition(0, VRows + 3);
        string addr = $"{(char)('A' + curC)}{curR + 1}";
        Console.Write($"[{addr}] > {(editing ? input : Grid[curR, curC].Formula)}".PadRight(Console.WindowWidth));
    }

    static void HandleNav(ConsoleKeyInfo k)
    {
        if (k.Key == ConsoleKey.Enter) editing = true;
        if (k.Key == ConsoleKey.UpArrow) curR = Math.Max(0, curR - 1);
        if (k.Key == ConsoleKey.DownArrow) curR = Math.Min(Rows - 1, curR + 1);
        if (k.Key == ConsoleKey.LeftArrow) curC = Math.Max(0, curC - 1);
        if (k.Key == ConsoleKey.RightArrow) curC = Math.Min(Cols - 1, curC + 1);
        
        if (curR < offR) offR = curR; else if (curR >= offR + VRows) offR = curR - VRows + 1;
        if (curC < offC) offC = curC; else if (curC >= offC + VCols) offC = curC - VCols + 1;
    }

    static void HandleEdit(ConsoleKeyInfo k)
    {
        if (k.Key == ConsoleKey.Enter) {
            Grid[curR, curC].Formula = input;
            Recalc();
            editing = false; input = "";
        }
        else if (k.Key == ConsoleKey.Escape) { editing = false; input = ""; }
        else if (k.Key == ConsoleKey.Backspace && input.Length > 0) input = input[..^1];
        else if (!char.IsControl(k.KeyChar)) input += k.KeyChar;
    }

    static void Recalc() { foreach (var c in Grid) c.Calculate(Grid); }
}

public class Cell
{
    public string Formula = "", Display = "";
    public void Calculate(Cell[,] grid)
    {
        if (string.IsNullOrEmpty(Formula)) { Display = ""; return; }
        if (!Formula.StartsWith("=")) { Display = Formula; return; }

        try {
            string expr = Regex.Replace(Formula[1..], "[A-Z][0-9]+", m => {
                int c = m.Value[0] - 'A', r = int.Parse(m.Value[1..]) - 1;
                return double.TryParse(grid[r, c].Display, out double v) ? v.ToString() : "0";
            });
            Display = new DataTable().Compute(expr, null)?.ToString() ?? "";
        } catch { Display = "#ERR"; }
    }
}