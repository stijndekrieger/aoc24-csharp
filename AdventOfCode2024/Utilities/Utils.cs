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

    public static char[,] GetGridFromTextInput(string[] input)
    {
        int rows = input.Length;
        int cols = input[0].Length;
        char[,] grid = new char[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                grid[row, col] = input[row][col];
            }
        }

        return grid;
    }

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
}
