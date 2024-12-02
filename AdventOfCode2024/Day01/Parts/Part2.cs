using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Day01.Parts;

public static class Day01Part2
{
    public static int Run()
    {
        (var list1, var list2) = Utils.GetListsFromTextInput(File.ReadAllLines("Day01/Data/Input.txt"));

        var similarityScore = 0;
        var listLength = list1.Count;

        for (var i = 0; i < listLength; i++)
        {
            var currentNumber = list1[i];
            var amount = list2.Count(number => number == currentNumber);
            var score = amount * currentNumber;

            similarityScore += score;
        }

        return similarityScore;
    }
}
