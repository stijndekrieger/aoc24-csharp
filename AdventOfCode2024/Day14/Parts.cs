using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day14;

public class Day14
{
    private static readonly int GridTilesWide = 101;
    private static readonly int GridTilesTall = 103;

    public static long Part1()
    {
        var robots = GetRobotsFromInput();

        for (var i = 0; i < 100; i++)
        {
            foreach (var robot in robots)
            {
                MoveRobot(robot);
            }
        }

        return CalculateSafetyFactor(robots);
    }

    public static int Part2()
    {
        var robots = GetRobotsFromInput();
        (int seconds, long lowestSafetyFactor) = (0, long.MaxValue);

        for (var i = 0; i < 10e3; i++)
        {
            foreach (var robot in robots)
            {
                MoveRobot(robot);
            }

            var safetyFactor = CalculateSafetyFactor(robots);
            if (safetyFactor < lowestSafetyFactor)
            {
                lowestSafetyFactor = safetyFactor;
                seconds = i + 1;
            }
        }

        return seconds;
    }

    private static long CalculateSafetyFactor(List<Robot> robots)
    {
        var quadrant1 = robots.Where(r => r.CurrentPosition.X < ((GridTilesWide - 1) / 2) && r.CurrentPosition.Y < ((GridTilesTall - 1) / 2)).Count();
        var quadrant2 = robots.Where(r => r.CurrentPosition.X > ((GridTilesWide - 1) / 2) && r.CurrentPosition.Y < ((GridTilesTall - 1) / 2)).Count();
        var quadrant3 = robots.Where(r => r.CurrentPosition.X < ((GridTilesWide - 1) / 2) && r.CurrentPosition.Y > ((GridTilesTall - 1) / 2)).Count();
        var quadrant4 = robots.Where(r => r.CurrentPosition.X > ((GridTilesWide - 1) / 2) && r.CurrentPosition.Y > ((GridTilesTall - 1) / 2)).Count();

        return quadrant1 * quadrant2 * quadrant3 * quadrant4;
    }

    private static void MoveRobot(Robot robot)
    {
        var newX = robot.CurrentPosition.X + robot.Velocity.X;
        var newY = robot.CurrentPosition.Y + robot.Velocity.Y;

        // Set new X coordinate
        if (newX < 0)
        {
            robot.CurrentPosition.X = GridTilesWide + newX;
        }
        else if (newX >= GridTilesWide)
        {
            robot.CurrentPosition.X = newX - GridTilesWide;
        }
        else
        {
            robot.CurrentPosition.X = newX;
        }

        // Set new Y coordinate
        if (newY < 0)
        {
            robot.CurrentPosition.Y = GridTilesTall + newY;
        }
        else if (newY >= GridTilesTall)
        {
            robot.CurrentPosition.Y = newY - GridTilesTall;
        }
        else
        {
            robot.CurrentPosition.Y = newY;
        }
    }

    private static void PrintGrid(List<Robot> robots)
    {
        Console.WriteLine("");
        for (int y = 0; y < GridTilesTall; y++)
        {
            for (int x = 0; x < GridTilesWide; x++)
            {
                var robotCount = robots.Where(r => r.CurrentPosition.X == x && r.CurrentPosition.Y == y).Count();
                if (robotCount > 0)
                {
                    Console.Write(robotCount);
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.Write("\n");
        }
        Console.WriteLine("");
    }

    private static List<Robot> GetRobotsFromInput()
    {
        var robots = new List<Robot>();
        var data = File.ReadAllLines("Day14/Data/Input.txt");

        var regex = new Regex(@"p=(-?[\d]+),(-?[\d]+) v=(-?[\d]+),(-?[\d]+)");

        foreach (var line in data)
        {
            var robot = new Robot();
            var regexMatches = regex.Matches(line);

            foreach (Match match in regexMatches)
            {
                robot.StartPosition.X = int.Parse(match.Groups[1].Value);
                robot.StartPosition.Y = int.Parse(match.Groups[2].Value);
                robot.CurrentPosition = robot.StartPosition;
                robot.Velocity.X = int.Parse(match.Groups[3].Value);
                robot.Velocity.Y = int.Parse(match.Groups[4].Value);
            }

            robots.Add(robot);
        }

        return robots;
    }

    private class Robot
    {
        public (int X, int Y) StartPosition;
        public (int X, int Y) CurrentPosition;
        public (int X, int Y) Velocity;
    }
}
