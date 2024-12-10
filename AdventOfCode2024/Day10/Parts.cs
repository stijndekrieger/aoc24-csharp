using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Day10;

public class Day10
{
    private static readonly char[,] TopograpgyMap = Utils.GetGridFromTextInput(File.ReadAllLines("Day10/Data/Input.txt"));

    public static int Part1()
    {
        var trailheadPositions = GetAllTrailheadPositions();
        var totalTrailheadScore = 0;

        foreach (var trailheadPosition in trailheadPositions) totalTrailheadScore += GetTrailheadScore(trailheadPosition);

        return totalTrailheadScore;
    }

    public static int Part2()
    {
        return 0;
    }

    public static int GetTrailheadScore((int row, int col) trailheadPosition)
    {
        List<(int, int)> positionsWhereNineWasReached = [];

        TraverseGrid(trailheadPosition.row, trailheadPosition.col, [trailheadPosition], positionsWhereNineWasReached);

        return positionsWhereNineWasReached.Distinct().Count();
    }

    private static void TraverseGrid(int currentRow, int currentCol, List<(int, int)> visitedPositions, List<(int, int)> positionsWhereNineWasReached)
    {
        var currentNumber = TopograpgyMap[currentRow, currentCol];
        var validNeighbours = GetValidNeighbours(currentRow, currentCol);

        if (validNeighbours.Count == 0)
        {
            if (currentNumber == '9') positionsWhereNineWasReached.Add((currentRow, currentCol));
            return;
        }

        foreach ((int row, int col) neighbour in validNeighbours)
        {
            if (!visitedPositions.Contains(neighbour))
            {
                var newVisitedPositions = new List<(int, int)>(visitedPositions) { neighbour };

                TraverseGrid(neighbour.row, neighbour.col, newVisitedPositions, positionsWhereNineWasReached);
            }
        }
    }

    private static List<(int, int)> GetValidNeighbours(int currentRow, int currentCol)
    {
        List<(int, int)> validNeighbours = [];
        var currentNumber = TopograpgyMap[currentRow, currentCol];

        foreach (var (dx, dy) in Directions)
        {
            var newRow = currentRow + dx;
            var newCol = currentCol + dy;

            // Out of bounds check
            if (newRow < 0 || newRow >= TopograpgyMap.GetLength(0) || newCol < 0 || newCol >= TopograpgyMap.GetLength(1)) continue;

            if (TopograpgyMap[newRow, newCol] - currentNumber == 1) validNeighbours.Add((newRow, newCol));
        }

        return validNeighbours;
    }

    private static List<(int, int)> GetAllTrailheadPositions()
    {
        int rows = TopograpgyMap.GetLength(0);
        int cols = TopograpgyMap.GetLength(1);
        List<(int, int)> positions = [];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (TopograpgyMap[i, j] == '0')
                {
                    positions.Add((i, j));
                }
            }
        }

        return positions;
    }

    private static readonly (int dx, int dy)[] Directions = {
        (1, 0),   // Right
        (-1, 0),  // Left
        (0, 1),   // Down
        (0, -1),  // Up
    };
}
