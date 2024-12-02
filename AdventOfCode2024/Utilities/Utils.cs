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

    public static List<List<int>> GetNumberListFromTextInput(string[] input)
    {
        List<List<int>> list = [];

        foreach (var line in input)
        {
            List<int> numberList = [];
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in numbers)
            {
                numberList.Add(int.Parse(item));
            }
            list.Add(numberList);
        }

        return list;
    }
}
