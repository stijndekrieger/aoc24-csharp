namespace AdventOfCode2024.Day11;

public class Day11
{
    private static readonly List<long> Stones = File.ReadAllText("Day11/Data/Input.txt").Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
    private static readonly int MULTIPLY_NUMBER = 2024;

    public static long Part1()
    {
        var stones = new List<long>(Stones);

        for (int i = 0; i < 25; i++) stones = Blink(stones);

        return stones.Count;
    }

    public static long Part2()
    {
        var stones = new List<long>(Stones);

        for (int i = 0; i < 75; i++)
        {
            stones = Blink(stones);
            Console.WriteLine($"Currently on blink {i + 1}");
        }

        return stones.Count;
    }

    private static List<long> Blink(List<long> stones)
    {
        var stonesAfterBlink = new List<long>();

        foreach (var stone in stones)
        {
            // Check which operation to do
            if (stone == 0) stonesAfterBlink.Add(1);
            else if (stone.ToString().Length % 2 == 0)
            {
                var newStone = stone.ToString();
                int halfLength = newStone.Length / 2;

                stonesAfterBlink.Add(long.Parse(newStone[..halfLength]));
                stonesAfterBlink.Add(long.Parse(newStone[halfLength..]));
            }
            else stonesAfterBlink.Add(stone * MULTIPLY_NUMBER);
        }

        return stonesAfterBlink;
    }
}
