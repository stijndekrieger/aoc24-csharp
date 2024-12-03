using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day03.Parts;

public static class Day03Part2
{
    public static int Run()
    {
        var input = string.Join("", File.ReadAllLines("Day03/Data/Input.txt"));

        var regex = new Regex(@"mul\(([0-9]{1,3}),([0-9]{1,3})\)|don't\(\)|do\(\)");
        var matches = regex.Matches(input);

        var total = 0;
        var doMull = true;

        foreach (Match match in matches)
        {
            if (match.Groups[0].Value == "don't()") doMull = false;
            else if (match.Groups[0].Value == "do()") doMull = true;
            else if (doMull) total += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }

        return total;
    }
}
