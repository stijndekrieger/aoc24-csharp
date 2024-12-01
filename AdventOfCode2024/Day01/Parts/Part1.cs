using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Day01.Parts;

public static class Part1
{
    public static int Run()
    {
        (var list1, var list2) = Utils.GetListsFromTextInput(File.ReadAllLines("Day01\\Data\\lists.txt"));

        var totalDifference = 0;
        var listLength = list1.Count;

        for (var i = 0; i < listLength; i++)
        {
            var list1Min = list1.Min();
            var list2Min = list2.Min();
            var difference = int.Abs(list1Min - list2Min);
            totalDifference += difference;
            list1.Remove(list1.Min());
            list2.Remove(list2.Min());
        }

        return totalDifference;
    }
}
