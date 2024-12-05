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

        var middleNumberCount = 0;

        foreach (var page in incorrectPages) middleNumberCount += Day05Utils.GetMiddleNumber(FixPageOrder(page, pageOrderingRules));

        return middleNumberCount;
    }

    private static List<int> FixPageOrder(List<int> page, List<(int, int)> pageOrderingRules)
    {
        var fixedPage = new List<int>(page);

        var relevantRules = pageOrderingRules
            .Where(rule => page.Contains(rule.Item1) && page.Contains(rule.Item2))
            .ToList();

        foreach (var rule in relevantRules)
        {
            int index1 = fixedPage.IndexOf(rule.Item1);
            int index2 = fixedPage.IndexOf(rule.Item2);

            if (index1 > index2)
            {
                fixedPage.RemoveAt(index1);
                fixedPage.Insert(index2, rule.Item1);

                return FixPageOrder(fixedPage, pageOrderingRules);
            }
        }

        return fixedPage;
    }
}