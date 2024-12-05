namespace AdventOfCode2024.Day05.Shared;

internal static class Day05Utils
{
    public static (List<(int, int)>, List<List<int>>) GetTupleListsFromTextInput(string[] input)
    {
        int separatorIndex = Array.IndexOf(input, "");

        var pageOrderingRules = input.Take(separatorIndex)
            .Select(line => line.Split('|').Select(int.Parse).ToArray())
            .Select(arr => (arr[0], arr[1]))
            .ToList();

        var pagesToProduce = input
            .Skip(separatorIndex + 1)
            .Select(line => line.Split(',').Select(int.Parse).ToList())
            .ToList();

        return (pageOrderingRules, pagesToProduce);
    }

    public static bool IsValidPage(List<int> page, List<(int, int)> pageOrderingRules)
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

    public static int GetMiddleNumber(List<int> page)
    {
        return page[page.Count / 2];
    }
}
