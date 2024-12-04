using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Day04.Parts;

public class Day04Part2
{
    // Three letter word to search in X form
    private static readonly string Word = "MAS";

    public static int Run()
    {
        var grid = Utils.GetGridFromTextInput(File.ReadAllLines("Day04/Data/Input.txt"));

        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        int wordXCount = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (grid[row, col] == Word[1]) if (IsWordXInPosition(grid, row, col)) wordXCount++;
            }
        }

        return wordXCount;
    }

    private static readonly ((int dx, int dy), (int dx, int dy))[] Directions = [
        ((-1,-1),(1,1)), // Up-Left & Down-Right
        ((1,-1),(-1,1)), // Up-Right & Down-Left
    ];

    private static bool IsWordXInPosition(char[,] grid, int startRow, int startCol)
    {
        // Check if the word X can fit
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        if (startRow == 0 || startRow >= rows - 1 || startCol == 0 || startCol >= cols - 1) return false;

        var wordDirectionCount = 0;

        foreach (var ((firstDX, firstDY), (secondDX, secondDY)) in Directions)
        {
            bool firstLetterFound = false;
            bool lastLetterFound = false;

            if (grid[startRow + firstDX, startCol + firstDY] == Word[0] || grid[startRow + secondDX, startCol + secondDY] == Word[0]) firstLetterFound = true;
            if (grid[startRow + firstDX, startCol + firstDY] == Word[2] || grid[startRow + secondDX, startCol + secondDY] == Word[2]) lastLetterFound = true;

            if (firstLetterFound && lastLetterFound) wordDirectionCount++;
        }

        return wordDirectionCount == 2;
    }
}
