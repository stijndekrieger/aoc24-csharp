using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Day15;

public class Day15
{
    private static readonly char[,] Grid = Utils.GetGridFromTextInput(File.ReadAllLines("Day15/Data/Input.txt").TakeWhile(line => !string.IsNullOrWhiteSpace(line)).ToArray());
    private static readonly string Commands = File.ReadAllText("Day15/Data/Input.txt").Split([' '], StringSplitOptions.RemoveEmptyEntries).Last();

    public static long Part1()
    {
        Console.WriteLine("Initial State:");
        PrintGrid();

        foreach (char command in Commands)
        {
            MoveRobot(command);
            //PrintGrid();
        }

        Console.WriteLine("Final State:");
        PrintGrid();

        return GetGpsScore();
    }

    public static int Part2()
    {
        return 0;
    }

    private static void MoveRobot(char command)
    {
        (int currentRow, int currentCol) = GetRobotPosition();
        int newRow = 0, newCol = 0;

        switch (command)
        {
            case '<':
                //Console.WriteLine("Move <:");
                newRow = currentRow;
                newCol = currentCol - 1;
                break;
            case '^':
                //Console.WriteLine("Move ^:");
                newRow = currentRow - 1;
                newCol = currentCol;
                break;
            case '>':
                //Console.WriteLine("Move >:");
                newRow = currentRow;
                newCol = currentCol + 1;
                break;
            case 'v':
                //Console.WriteLine("Move v:");
                newRow = currentRow + 1;
                newCol = currentCol;
                break;
        }

        // When moving into a wall, nothing happens so it's not in the switch statement
        switch (Grid[newRow, newCol])
        {
            case '.':
                Grid[newRow, newCol] = '@';
                Grid[currentRow, currentCol] = '.';
                break;
            case 'O':
                HandleBoxPush(newRow, newCol, currentRow, currentCol, command);
                break;
        }
    }

    private static void HandleBoxPush(int boxRow, int boxCol, int robotRow, int robotCol, char command)
    {
        (int currentRow, int currentCol) = (boxRow, boxCol);

        while (true)
        {

            switch (command)
            {
                case '<':
                    currentCol = currentCol - 1;
                    break;
                case '^':
                    currentRow = currentRow - 1;
                    break;
                case '>':
                    currentCol = currentCol + 1;
                    break;
                case 'v':
                    currentRow = currentRow + 1;
                    break;
            }

            // If it's another box, continue, otherwise break the loop
            if (Grid[currentRow, currentCol] == '.')
            {
                Grid[boxRow, boxCol] = '@';
                Grid[robotRow, robotCol] = '.';
                Grid[currentRow, currentCol] = 'O';
                break;
            }
            else if (Grid[currentRow, currentCol] == '#')
            {
                break;
            }
        }
    }

    private static long GetGpsScore()
    {
        long score = 0;

        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                if (Grid[i, j] == 'O')
                {
                    score += 100 * i + j;
                }
            }
        }

        return score;
    }

    private static (int, int) GetRobotPosition()
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                if (Grid[i, j] == '@')
                {
                    return (i, j);
                }
            }
        }

        return (0, 0);
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
        Console.WriteLine();
    }
}
