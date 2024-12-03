using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day03.Parts;

public static class Day03Part1
{
    public static int Run()
    {
        var input = string.Join("", File.ReadAllLines("Day03/Data/Input.txt"));

        var regex = new Regex(@"mul\(([0-9]{1,3}),([0-9]{1,3})\)");
        var matches = regex.Matches(input);

        var total = matches.Sum(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value));

        return total;
    }
}
