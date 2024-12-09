namespace AdventOfCode2024.Day09;

public class Day09
{
    private static readonly string DiskMap = File.ReadAllText("Day09/Data/Input.txt");

    public static long Part1()
    {
        List<int> diskMapBlocks = GetDiskMapBlocks();
        //Console.WriteLine(string.Join(" ", diskMapBlocks.Select(x => x == -1 ? "." : x.ToString())));
        //Console.WriteLine();
        var fixedDiskMap = FixDiskMap(diskMapBlocks);
        //Console.WriteLine(string.Join(" ", fixedDiskMap.Select(x => x == -1 ? "." : x.ToString())));

        return CalculateChecksum(fixedDiskMap);
    }

    public static long Part2()
    {
        return 0;
    }

    private static List<int> FixDiskMap(List<int> diskMapBlocks)
    {
        var newDiskMap = new List<int>(diskMapBlocks);

        // Reverse loop to check for non-. characters
        for (int i = diskMapBlocks.Count - 1; i >= 0; i--)
        {
            if (diskMapBlocks[i] != -1)
            {
                for (int j = 0; j < diskMapBlocks.Count; j++)
                {
                    if (newDiskMap[j] == -1)
                    {
                        // Replace . character with number
                        newDiskMap[j] = diskMapBlocks[i];
                        // Remove number from the end
                        newDiskMap[i] = -1;
                        break;
                    }
                }
            }

            if (IsDiskMapFixed(newDiskMap)) break;
        }

        return newDiskMap;
    }

    private static bool IsDiskMapFixed(List<int> diskMap)
    {
        int firstDotIndex = diskMap.IndexOf(-1);

        if (firstDotIndex == -1) return true;
        for (int i = firstDotIndex; i < diskMap.Count; i++) if (diskMap[i] != -1) return false;

        return true;
    }

    private static long CalculateChecksum(List<int> fixedDiskMap)
    {
        long checksum = 0;

        for (int i = 0; i < fixedDiskMap.Count; i++)
            if (fixedDiskMap[i] != -1)
            {
                checksum += (long)i * fixedDiskMap[i];
                //Console.WriteLine($"New calculation: index {i} x number {int.Parse(fixedDiskMap[i].ToString())} = {i * int.Parse(fixedDiskMap[i].ToString())}");
            }

        return checksum;
    }

    private static List<int> GetDiskMapBlocks()
    {
        var diskMapBlocks = new List<int>();

        for (int i = 0; i < DiskMap.Length; i++)
        {
            if (i % 2 == 0)
            {
                // New ID number
                var id = i / 2;
                int number = int.Parse(DiskMap[i].ToString());
                for (int j = 0; j < number; j++) diskMapBlocks.Add(id);
            }
            else
            {
                // Digit indicating space
                int number = int.Parse(DiskMap[i].ToString());
                for (int j = 0; j < number; j++) diskMapBlocks.Add(-1);
            }
        }

        return diskMapBlocks;
    }
}
