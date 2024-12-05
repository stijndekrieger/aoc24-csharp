using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Day05.Parts;

public class Day05Part1
{
    public static int Run()
    {
        var input = File.ReadAllLines("Day05/Data/Input.txt");
        var (pageOrderingRules, pagesToProduce) = Utils.GetTupleListsFromTextInput(input);

        var middleNumberCount = 0;

        foreach (var page in pagesToProduce) if (IsValidPage(page, pageOrderingRules)) middleNumberCount += GetMiddleNumber(page);

        return middleNumberCount;
    }

    private static bool IsValidPage(List<int> page, List<(int, int)> pageOrderingRules)
    {
        var isValid = true;

        var relevantRules = pageOrderingRules
            .Where(rule => page.Contains(rule.Item1) && page.Contains(rule.Item2))
            .ToList();

        foreach (var rule in relevantRules)
        {
            if (page.IndexOf(rule.Item1) > page.IndexOf(rule.Item2))
            {
                isValid = false;
                break;
            }
        }

        return isValid;
    }

    private static int GetMiddleNumber(List<int> page)
    {
        return page[page.Count / 2];
    }
}
