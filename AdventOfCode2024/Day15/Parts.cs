using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Day15;

public class Day15
{
    private static readonly char[,] Grid = Utils.GetGridFromTextInput(File.ReadAllLines("Day15/Data/Input.txt").TakeWhile(line => !string.IsNullOrWhiteSpace(line)).ToArray());
    private static readonly string Commands = File.ReadAllText("Day15/Data/Input.txt").Split(['\n'], StringSplitOptions.RemoveEmptyEntries).Last();

    public static int Part1()
    {
        return 0;
    }

    public static int Part2()
    {
        return 0;
    }

    private static void PrintGrid()
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                Console.Write(Grid[i, j]);
            }
            Console.WriteLine();
        }
    }
}
