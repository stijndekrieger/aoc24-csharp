namespace AdventOfCode2024.Day07;

public class Day07
{
    public static long Part1()
    {
        return SolveCalibrationPuzzle(File.ReadAllLines("Day07/Data/Input.txt"));
    }

    public static long Part2()
    {
        return SolveCalibrationPuzzle(File.ReadAllLines("Day07/Data/Input.txt"), true);
    }

    public static long SolveCalibrationPuzzle(string[] equations, bool extraOperator = false)
    {
        long totalCalibrationResult = 0;

        foreach (string equation in equations)
        {
            var parts = equation.Split(':');
            long testValue = long.Parse(parts[0]);
            long[] numbers = parts[1].Trim().Split(' ').Select(long.Parse).ToArray();

            if (IsValidEquation(numbers, testValue, extraOperator))
            {
                totalCalibrationResult += testValue;
            }
        }

        return totalCalibrationResult;
    }

    private static bool IsValidEquation(long[] numbers, long targetValue, bool extraOperator)
    {
        // We need one less operator than numbers
        int operatorPositions = numbers.Length - 1;

        // Generate all possible operator combinations (+ or *)
        foreach (var operatorConfig in GenerateOperatorConfigurations(operatorPositions, extraOperator))
        {
            if (EvaluateExpression(numbers, operatorConfig) == targetValue)
            {
                return true;
            }
        }

        return false;
    }

    private static IEnumerable<char[]> GenerateOperatorConfigurations(int positions, bool extraOperator)
    {
        char[] operators;
        if (extraOperator)
        {
            operators = new[] { '+', '*', '|' };
        }
        else
        {
            operators = new[] { '+', '*' };
        }

        return GetCombinations(operators, positions);
    }

    private static IEnumerable<char[]> GetCombinations(char[] elements, int length)
    {
        if (length == 1)
        {
            return elements.Select(e => new[] { e });
        }

        return GetCombinations(elements, length - 1)
            .SelectMany(
                prev => elements,
                (prev, curr) => prev.Append(curr).ToArray());
    }

    private static long EvaluateExpression(long[] numbers, char[] operators)
    {
        long result = numbers[0];
        for (int i = 0; i < operators.Length; i++)
        {
            if (operators[i] == '+')
            {
                result += numbers[i + 1];
            }
            else if (operators[i] == '|')
            {
                result = ConcatenateNumbers(result, numbers[i + 1]);
            }
            else // '*'
            {
                result *= numbers[i + 1];
            }
        }
        return result;
    }

    private static long ConcatenateNumbers(long left, long right)
    {
        // Convert numbers to strings to concatenate
        string leftStr = left.ToString();
        string rightStr = right.ToString();
        return long.Parse(leftStr + rightStr);
    }
}
