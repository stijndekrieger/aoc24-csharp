using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Day02.Parts;

public static class Day02Part2
{
    public static int Run()
    {
        var reports = Utils.GetNumberListFromTextInput(File.ReadAllLines("Day02/Data/Input.txt"));

        var safeReports = 0;

        foreach (var report in reports)
        {
            var isValid = CheckReport(report);
            if (isValid) safeReports++;
        }

        return safeReports;
    }

    private static bool CheckReport(List<int> report, bool allowBadIndicesCheck = true)
    {
        var isValid = true;
        var orders = new Dictionary<int, Order>();
        var badIndices = false;

        for (var i = 0; i < report.Count - 1; i++)
        {
            var isCurrentLevelValid = true;
            var level = report[i];
            var nextLevel = report[i + 1];
            var (difference, deltaOrder) = CalculateDifference(level, nextLevel);

            orders.Add(i, deltaOrder);

            if (difference == 0 || difference > 3) badIndices = true;
        }

        if (orders.GroupBy(x => x.Value).Count() == 2) badIndices = true;

        if (allowBadIndicesCheck && badIndices)
        {
            for (var i = 0; i < report.Count; i++)
            {
                var alteredReport = new List<int>(report);
                alteredReport.RemoveAt(i);
                var succeeded = CheckReport(alteredReport, false);
                if (succeeded) return true;
            }
        }

        if (badIndices) isValid = false;

        return isValid;
    }

    private static (int difference, Order order) CalculateDifference(int previous, int next)
    {
        var difference = previous - next;
        var order = difference == 0 ? Order.Equal : difference > 0 ? Order.Descending : Order.Ascending;
        return (Math.Abs(difference), order);
    }

}

public enum Order
{
    Equal,
    Ascending,
    Descending
}
