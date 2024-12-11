namespace AdventOfCode2024.Day11;

public class Day11
{
    private static readonly ulong[] Stones = File.ReadAllText("Day11/Data/Input.txt").Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => ulong.Parse(s)).ToArray();
    private static readonly Dictionary<(ulong, int), ulong> Cache = [];

    public static ulong Part1()
    {
        Cache.Clear();
        var stones = new ulong[Stones.Length];
        Array.Copy(Stones, stones, Stones.Length);

        return stones.Aggregate((ulong)0, (acc, s) => acc + Blink(s, 25));
    }

    public static ulong Part2()
    {
        Cache.Clear();
        var stones = new ulong[Stones.Length];
        Array.Copy(Stones, stones, Stones.Length);

        return stones.Aggregate((ulong)0, (acc, s) => acc + Blink(s, 75));
    }

    private static ulong Blink(ulong stone, int count)
    {
        var tuple = (stone, count);

        if (Cache.TryGetValue(tuple, out var value)) return value;

        ulong result;

        if (count == 0) result = 1; // Check for end of recursion
        else if (stone == 0) result = Blink(1, count - 1);
        else result = TransformStone(stone, count);

        Cache.Add(tuple, result);
        return result;
    }

    private static ulong TransformStone(ulong stone, int count)
    {
        int numberOfDigits = stone.ToString().Length;

        if (numberOfDigits % 2 != 0) return Blink(stone * 2024, count - 1);

        ulong left = stone / (ulong)Math.Pow(10, numberOfDigits / 2);
        ulong right = stone % (ulong)Math.Pow(10, numberOfDigits / 2);

        return Blink(left, count - 1) + Blink(right, count - 1);
    }
}
