using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day13;

public class Day13
{
    public static int Part1()
    {
        var machines = GetMachinesFromInput();
        var totalPrice = 0;

        foreach (var machine in machines) totalPrice += GetCheapestCombination(machine);

        return totalPrice;
    }

    public static int Part2()
    {
        return 0;
    }

    private static int GetCheapestCombination(Machine machine)
    {
        const int ButtonAPrice = 3;
        const int ButtonBPrice = 1;
        (int X, int Y) currentPosition = (0, 0);
        var buttonAPresses = 0;
        var buttonBPresses = 0;

        // TODO: Calculate least amount of presses to get to machine's prize
        // Math???????
        // If (not possible) return 0;

        return (buttonAPresses * ButtonAPrice) + (buttonBPresses * ButtonBPrice);
    }

    private static List<Machine> GetMachinesFromInput()
    {
        var machines = new List<Machine>();
        var data = File.ReadAllText("Day13/Data/Input.txt");

        // Concat every 3 lines together to parse with regex later
        var machineInput = data
            .Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries)
            .Select((line, index) => new { line, index })
            .GroupBy(x => x.index / 3)
            .Select(group => string.Concat(group.Select(x => x.line)))
            .ToArray();

        var buttonRegex = new Regex(@"Button ([AB]): X\+(\d*), Y\+(\d*)");
        var prizeRegex = new Regex(@"Prize: X=(\d*), Y=(\d*)");

        foreach (var machineText in machineInput)
        {
            var machine = new Machine();
            var buttonMatches = buttonRegex.Matches(machineText);
            var prizeMatches = prizeRegex.Matches(machineText);

            foreach (Match buttonMatch in buttonMatches)
            {
                if (buttonMatch.Groups[1].Value == "A")
                {
                    machine.ButtonA.dx = int.Parse(buttonMatch.Groups[2].Value);
                    machine.ButtonA.dy = int.Parse(buttonMatch.Groups[3].Value);
                }
                else
                {
                    machine.ButtonB.dx = int.Parse(buttonMatch.Groups[2].Value);
                    machine.ButtonB.dy = int.Parse(buttonMatch.Groups[3].Value);
                }
            }

            foreach (Match prizeMatch in prizeMatches)
            {
                machine.Prize.X = int.Parse(prizeMatch.Groups[1].Value);
                machine.Prize.Y = int.Parse(prizeMatch.Groups[2].Value);
            }

            machines.Add(machine);
        }

        return machines;
    }

    private class Machine
    {
        public (int dx, int dy) ButtonA;
        public (int dx, int dy) ButtonB;
        public (int X, int Y) Prize;
    }
}
