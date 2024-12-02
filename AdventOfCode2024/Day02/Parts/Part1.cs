using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Day02.Parts;

public static class Day02Part1
{
    public static int Run()
    {
        var reports = Utils.GetNumberListFromTextInput(File.ReadAllLines("Day02/Data/Input.txt"));

        int safeReports = 0;

        foreach (var report in reports)
        {
            int? previousNumber = null;
            bool? descending = null;
            bool isReportSafe = false;

            foreach (var level in report)
            {
                if (previousNumber == null) // First iteration
                {
                    previousNumber = level;
                    continue;
                }

                if (descending == null) // Second iteration
                {
                    if (previousNumber > level) descending = true;
                    else if (previousNumber < level) descending = false;
                    else
                    {
                        isReportSafe = false;
                        break;
                    }

                    if (!IsValidLevel(previousNumber.Value, level, descending.Value))
                    {
                        isReportSafe = false;
                        break;
                    }
                    previousNumber = level;
                    continue;
                }

                // All other iterations
                if (!IsValidLevel(previousNumber.Value, level, descending.Value))
                {
                    isReportSafe = false;
                    break;
                }
                isReportSafe = true;
                previousNumber = level;

            }

            if (isReportSafe) safeReports++;
        }

        return safeReports;
    }

    private static bool IsValidLevel(int previousNumber, int level, bool descending)
    {
        if (previousNumber == level) return false;

        if (int.Abs(previousNumber - level) > 3) return false;

        if (previousNumber > level && !descending) return false;
        if (previousNumber < level && descending) return false;

        return true;
    }
}
