using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Day12;

public class Day12
{
    private static readonly char[,] Garden = Utils.GetGridFromTextInput(File.ReadAllLines("Day12/Data/Input.txt"));

    public static int Part1()
    {
        int totalPrice = 0;

        foreach (var plot in GetGardenPlots()) totalPrice += GetPerimiter(plot) * plot.positions.Count;

        return totalPrice;
    }

    public static int Part2()
    {
        int totalPrice = 0;

        foreach (var plot in GetGardenPlots()) totalPrice += GetPlotSides(plot) * plot.positions.Count;

        return totalPrice;
    }

    #region Part 1 specific

    private static int GetPerimiter((char plant, List<(int row, int col)> positions) plot)
    {
        var perimiter = 0;

        foreach ((int row, int col) position in plot.positions)
        {
            perimiter += 4 - GetSamePlantNeighbours(position.row, position.col).Count;
        }

        return perimiter;
    }

    #endregion

    #region Part 2 specific

    private static int GetPlotSides((char plant, List<(int row, int col)> positions) plot)
    {
        var sides = new List<(Direction direction, List<(int row, int col)> positions)>();

        foreach ((int row, int col) in plot.positions)
        {
            var neighbours = GetSamePlantNeighbours(row, col);
            var openDirections = GetOpenDirections(row, col);
            foreach (var direction in openDirections)
            {
                var matchingSides = sides.Where(side => side.direction == direction && side.positions.Any(sidePosition => neighbours.Contains(sidePosition))).ToList();
                if (matchingSides.Count != 0)
                {
                    matchingSides.Single().positions.Add((row, col));
                }
                else
                {
                    var positions = new List<(int row, int col)>() { (row, col) };
                    sides.Add((direction, positions));
                }
            }
        }

        return sides.Count;
    }

    private static List<Direction> GetOpenDirections(int currentRow, int currentCol)
    {
        var currentPlant = Garden[currentRow, currentCol];
        var openDirections = new List<Direction>();

        for (int i = 0; i < Directions.Length; i++)
        {
            var (dx, dy) = Directions[i];
            var newRow = currentRow + dx;
            var newCol = currentCol + dy;

            var direction = (Direction)i;

            // Out of bounds check
            if (newRow < 0 || newRow >= Garden.GetLength(0) || newCol < 0 || newCol >= Garden.GetLength(1))
            {
                openDirections.Add(direction);
                continue;
            }

            if (Garden[newRow, newCol] != currentPlant) openDirections.Add(direction);
        }

        return openDirections;
    }

    #endregion

    private static List<(char plant, List<(int row, int col)> positions)> GetGardenPlots()
    {
        var plots = new List<(char plant, List<(int row, int col)> positions)>();

        for (int row = 0; row < Garden.GetLength(0); row++)
        {
            for (int col = 0; col < Garden.GetLength(1); col++)
            {
                var currentPlant = Garden[row, col];
                var samePlantNeighbours = GetSamePlantNeighbours(row, col);

                var matchingPlots = plots.Where(plot => plot.plant == currentPlant && plot.positions.Any(plant => samePlantNeighbours.Contains(plant))).ToList();
                // If there are plots in neighbouring positions with the same plant, add this plant to that plot
                if (matchingPlots.Count != 0)
                {
                    if (matchingPlots.Count > 1)
                    {
                        (char plant, List<(int row, int col)> positions) newPlot = (currentPlant, matchingPlots.SelectMany(s => s.positions).ToList());
                        newPlot.positions.Add((row, col));
                        foreach (var plot in matchingPlots) plots.Remove(plot);
                        plots.Add(newPlot);
                    }
                    else
                    {
                        matchingPlots.Single().positions.Add((row, col));
                    }
                }
                // If no plots exist with the same plant as neigbours, create a new plot
                else
                {
                    var positions = new List<(int row, int col)>() { (row, col) };
                    plots.Add((currentPlant, positions));
                }
            }
        }

        return plots;
    }

    private static List<(int, int)> GetSamePlantNeighbours(int currentRow, int currentCol)
    {
        List<(int, int)> samePlantNeigbours = [];
        var currentPlant = Garden[currentRow, currentCol];

        foreach (var (dx, dy) in Directions)
        {
            var newRow = currentRow + dx;
            var newCol = currentCol + dy;

            // Out of bounds check
            if (newRow < 0 || newRow >= Garden.GetLength(0) || newCol < 0 || newCol >= Garden.GetLength(1)) continue;

            if (Garden[newRow, newCol] == currentPlant) samePlantNeigbours.Add((newRow, newCol));
        }

        return samePlantNeigbours;
    }

    private static readonly (int dx, int dy)[] Directions = [
        (1, 0),   // Right
        (-1, 0),  // Left
        (0, 1),   // Down
        (0, -1),  // Up
    ];

    private enum Direction
    {
        Right = 0,
        Left = 1,
        Down = 2,
        Up = 3,
    }
}
