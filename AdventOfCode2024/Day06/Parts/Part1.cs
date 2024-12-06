using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Day06.Parts;

public class Day06Part1
{
    private static char[,] Grid { get; set; }
    private static GuardBase Guard { get; set; }

    public static int Run()
    {
        Grid = Utils.GetGridFromTextInput(File.ReadAllLines("Day06/data/Input.txt"));

        SetGuardStart();

        var visitedLocations = new List<(int, int)>();

        while (true)
        {
            if (IsCurrentPositionOutOfBounds()) break;

            visitedLocations.Add(Guard.Position);
            UpdateGuardPosition();
        }

        //PrintDebugGrid(visitedLocations);
        return visitedLocations.Distinct().Count();
    }

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left,
    }

    public class GuardBase
    {
        private int Row { get; set; }
        private int Col { get; set; }
        public Direction Direction { get; set; }
        public (int, int) Position
        {
            get => (Row, Col);
            set
            {
                Row = value.Item1;
                Col = value.Item2;
            }
        }
    }

    private static void SetGuardStart()
    {
        var rows = Grid.GetLength(0);
        var cols = Grid.GetLength(1);

        var guard = new GuardBase();

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                switch (Grid[row, col])
                {
                    case '^':
                        guard.Position = (row, col);
                        guard.Direction = Direction.Up;
                        break;
                    case '>':
                        guard.Position = (row, col);
                        guard.Direction = Direction.Right;
                        break;
                    case 'v':
                        guard.Position = (row, col);
                        guard.Direction = Direction.Down;
                        break;
                    case '<':
                        guard.Position = (row, col);
                        guard.Direction = Direction.Left;
                        break;
                }
            }
        }

        Guard = guard;
    }

    private static bool IsCurrentPositionOutOfBounds()
    {
        var rows = Grid.GetLength(0);
        var cols = Grid.GetLength(1);

        var (row, col) = Guard.Position;
        return row < 0 || row >= rows || col < 0 || col >= cols;
    }

    private static Direction GetNextDirection()
    {
        switch (Guard.Direction)
        {
            case Direction.Up:
                return Direction.Right;
            case Direction.Right:
                return Direction.Down;
            case Direction.Down:
                return Direction.Left;
            case Direction.Left:
                return Direction.Up;
            default:
                throw new Exception("Direction invalid");
        }
    }

    private static void UpdateGuardPosition()
    {
        var (previousRow, previousCol) = Guard.Position;
        switch (Guard.Direction)
        {
            case Direction.Up:
                Guard.Position = (previousRow - 1, previousCol);
                break;
            case Direction.Right:
                Guard.Position = (previousRow, previousCol + 1);
                break;
            case Direction.Down:
                Guard.Position = (previousRow + 1, previousCol);
                break;
            case Direction.Left:
                Guard.Position = (previousRow, previousCol - 1);
                break;
        }

        var (row, col) = Guard.Position;
        if (!IsCurrentPositionOutOfBounds())
        {
            if (Grid[row, col] == '#')
            {
                Guard.Position = (previousRow, previousCol);
                Guard.Direction = GetNextDirection();
            }
        }
    }

    private static void PrintDebugGrid(List<(int, int)> visitedLocations)
    {
        int rows = Grid.GetLength(0);
        int cols = Grid.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (visitedLocations.Contains((i, j))) Console.Write('X');
                else Console.Write(Grid[i, j]);
            }
            Console.WriteLine();
        }
    }
}
