using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Day04.Parts;

public class Day04Part1
{
    // Word to search
    private static readonly string Word = "XMAS";

    public static int Run()
    {
        var grid = Utils.GetGridFromTextInput(File.ReadAllLines("Day04/Data/Input.txt"));

        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        int xmasCount = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                xmasCount += CountXmasFromPosition(grid, row, col);
            }
        }

        return xmasCount;
    }

    private static readonly (int dx, int dy)[] Directions = {
        (1, 0),   // Right
        (-1, 0),  // Left
        (0, 1),   // Down
        (0, -1),  // Up
        (1, 1),   // Down-Right
        (-1, -1), // Up-Left
        (1, -1),  // Up-Right
        (-1, 1)   // Down-Left
    };

    private static int CountXmasFromPosition(char[,] grid, int startRow, int startCol)
    {
        int xmasCount = 0;

        foreach (var (dx, dy) in Directions) if (IsXmasAtDirection(grid, startRow, startCol, dx, dy)) xmasCount++;

        return xmasCount;
    }

    private static bool IsXmasAtDirection(char[,] grid, int startRow, int startCol, int dx, int dy)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        // Check if the entire word can fit in direction
        if (startRow + dx * 3 < 0 || startRow + dx * 3 >= rows || startCol + dy * 3 < 0 || startCol + dy * 3 >= cols) return false;

        for (int i = 0; i < Word.Length; i++)
        {
            int currentRow = startRow + i * dx;
            int currentCol = startCol + i * dy;

            if (grid[currentRow, currentCol] != Word[i]) return false;
        }

        return true;
    }
}
