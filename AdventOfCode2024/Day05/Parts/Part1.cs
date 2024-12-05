using AdventOfCode2024.Day05.Shared;

namespace AdventOfCode2024.Day05.Parts;

public class Day05Part1
{
    public static int Run()
    {
        var input = File.ReadAllLines("Day05/Data/Input.txt");
        var (pageOrderingRules, pagesToProduce) = Day05Utils.GetTupleListsFromTextInput(input);

        var middleNumberCount = 0;

        foreach (var page in pagesToProduce) if (Day05Utils.IsValidPage(page, pageOrderingRules)) middleNumberCount += Day05Utils.GetMiddleNumber(page);

        return middleNumberCount;
    }
}
