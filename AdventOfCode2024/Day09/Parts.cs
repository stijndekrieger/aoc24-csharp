namespace AdventOfCode2024.Day09;

public class Day09
{
    private static readonly string DiskMap = File.ReadAllText("Day09/Data/Input.txt");

    public static long Part1()
    {
        List<int> diskMapBlocks = GetDiskMapBlocks();
        var fixedDiskMap = FixDiskMapPart1(diskMapBlocks);

        return CalculateChecksum(fixedDiskMap);
    }

    public static long Part2()
    {
        List<int> diskMapBlocks = GetDiskMapBlocks();
        var fixedDiskMap = FixDiskMapPart2(diskMapBlocks);

        return CalculateChecksum(fixedDiskMap);
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

    #region Part 1 specific functions

    private static List<int> FixDiskMapPart1(List<int> diskMapBlocks)
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

    #endregion

    #region Part 2 specific functions

    private static List<int> FixDiskMapPart2(List<int> diskMapBlocks)
    {
        var newDiskMap = new List<int>(diskMapBlocks);

        var fileIds = newDiskMap
            .Where(x => x != -1)
            .OrderByDescending(x => x)
            .Distinct()
            .ToList();

        foreach (var fileId in fileIds)
        {
            var currentPositions = Enumerable.Range(0, newDiskMap.Count)
                .Where(i => newDiskMap[i] == fileId)
                .ToList();

            int startPos = currentPositions.Min();
            int endPos = currentPositions.Max();
            int fileSize = currentPositions.Count;

            int bestNewStart = FindBestFit(newDiskMap, fileSize);

            // If a suitable span is found, move the file
            if (bestNewStart != -1 && bestNewStart < startPos)
            {
                // Clear original positions
                for (int i = startPos; i <= endPos; i++)
                {
                    if (newDiskMap[i] == fileId)
                        newDiskMap[i] = -1;
                }

                // Fill new positions
                for (int i = 0; i < fileSize; i++)
                {
                    newDiskMap[bestNewStart + i] = fileId;
                }
            }
        }

        return newDiskMap;
    }

    private static int FindBestFit(List<int> diskMap, int requiredSize)
    {
        for (int start = 0; start <= diskMap.Count - requiredSize; start++)
        {
            bool validSpan = true;
            for (int i = start; i < start + requiredSize; i++)
            {
                if (diskMap[i] != -1)
                {
                    validSpan = false;
                    break;
                }
            }

            if (validSpan)
                return start;
        }

        return -1;
    }

    #endregion
}
