using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Day08;

public class Day08
{
    private static readonly char[,] Grid = Utils.GetGridFromTextInput(File.ReadAllLines("Day08/Data/Input.txt"));

    public static int Part1()
    {
        var antinodes = new List<(int, int)>();

        for (int row = 0; row < Grid.GetLength(0); row++)
        {
            for (int col = 0; col < Grid.GetLength(0); col++)
            {
                // New antenna found
                if (Grid[row, col] != '.') antinodes.AddRange(GetNewAntinodesForAntenna((row, col)));
            }
        }

        //PrintDebugGrid(antinodes);
        return antinodes.Distinct().Count();
    }

    public static int Part2()
    {
        return 0;
    }

    private static List<(int, int)> GetNewAntinodesForAntenna((int row, int col) antennaLocation)
    {
        var newAntinodes = new List<(int, int)>();
        char antenna = Grid[antennaLocation.Item1, antennaLocation.Item2];

        for (int row = 0; row < Grid.GetLength(0); row++)
        {
            for (int col = 0; col < Grid.GetLength(1); col++)
            {
                // Match for another antenna of same character, and not itself
                if (Grid[row, col] == antenna && (row, col) != antennaLocation)
                {
                    var otherAntennaLocation = (row, col);
                    var distance = (row: otherAntennaLocation.row - antennaLocation.row, col: otherAntennaLocation.col - antennaLocation.col);
                    var antinode = (row: otherAntennaLocation.row + distance.row, col: otherAntennaLocation.col + distance.col);
                    // If within bounds of the grid, add the antinode
                    if (antinode.row >= 0 && antinode.row < Grid.GetLength(0) && antinode.col >= 0 && antinode.col < Grid.GetLength(1)) newAntinodes.Add(antinode);
                }
            }
        }

        return newAntinodes;
    }

    private static void PrintDebugGrid(List<(int, int)> antinodes)
    {

        for (int row = 0; row < Grid.GetLength(0); row++)
        {
            for (int col = 0; col < Grid.GetLength(1); col++)
            {
                if (antinodes.Contains((row, col)))
                {
                    if (Grid[row, col] != '.') Console.Write(Grid[row, col]);
                    else Console.Write('#');
                }
                else Console.Write(Grid[row, col]);
            }
            Console.WriteLine();
        }
    }
}
