namespace AdventOfCode2024.Utilities;

public static class Utils
{
    public static (List<int>, List<int>) GetListsFromTextInput(string[] input)
    {
        List<int> list1 = [];
        List<int> list2 = [];

        foreach (var line in input)
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            list1.Add(int.Parse(numbers[0].Trim()));
            list2.Add(int.Parse(numbers[1].Trim()));
        }

        return (list1, list2);
    }
}
