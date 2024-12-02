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

    private static bool CheckReport(List<int> report, bool allowBadIndices = true)
    {
        var isValid = true;
        //var (_, order) = CalculateDifference(report.First(), report.Skip(1).First()); // TODO: don't lock order based on first result
        var orders = new Dictionary<int, Order>();
        var badIndices = new List<int>();

        for (var i = 0; i < report.Count - 1; i++)
        {
            var isCurrentLevelValid = true;
            var level = report[i];
            var nextLevel = report[i + 1];
            var (difference, deltaOrder) = CalculateDifference(level, nextLevel);

            orders.Add(i, deltaOrder);

            if (difference == 0 || difference > 3) isCurrentLevelValid = false;
            //if (deltaOrder != order) isCurrentLevelValid = false;

            if (!isCurrentLevelValid) badIndices.Add(i);
        }

        var groupedOrders = orders.GroupBy(x => x.Value);
        if (groupedOrders.Any(go => go.Count() == 1))
        {
            var badGroupedOrder = groupedOrders.Where(go => go.Count() == 1).First();
            var badIndex = badGroupedOrder.First().Key;
            if (!badIndices.Contains(badIndex)) badIndices.Add(badIndex);
        }

        if (allowBadIndices)
        {
            foreach (var badIndex in badIndices)
            {
                var alteredReport = report;
                alteredReport.RemoveAt(badIndex);
                var succeeded = CheckReport(alteredReport, false);
                if (succeeded) return true;
            }
        }

        if (badIndices.Count > 0) isValid = false;

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
