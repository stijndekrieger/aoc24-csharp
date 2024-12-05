using AdventOfCode2024.Day05.Shared;

namespace AdventOfCode2024.Day05.Parts;

public class Day05Part2
{
    public static int Run()
    {
        var input = File.ReadAllLines("Day05/Data/Input.txt");
        var (pageOrderingRules, pagesToProduce) = Day05Utils.GetTupleListsFromTextInput(input);

        var incorrectPages = new List<List<int>>();

        foreach (var page in pagesToProduce) if (!Day05Utils.IsValidPage(page, pageOrderingRules)) incorrectPages.Add(page);

        return 0;
    }
}