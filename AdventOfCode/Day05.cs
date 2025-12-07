using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;

[RunTest]
public sealed class Day05 : TestableDay
{
    private readonly (ulong start, ulong end)[] _freshRanges;
    private readonly ulong[] _ingredients;

    public Day05()
    {
        string input = File.ReadAllText(InputFilePath);
        string[] halves = input.Split("\r\n\r\n");
        _freshRanges = halves[0].Split("\r\n").Select(ParseRange).ToArray();
        _ingredients = halves[1].Split("\r\n").Select(ulong.Parse).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        ulong freshCount = 0;
        foreach (ulong ingredient in _ingredients)
        {
            if (_freshRanges.Any(range => CheckInRange(range.start, range.end, ingredient)))
            {
                freshCount++;
            }
        }
        return new ValueTask<string>(freshCount.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var ranges = _freshRanges.ToList();

        bool didMerge = true;
        while (didMerge)
        {
            didMerge = false;
            List<(ulong start, ulong end)> toRemove = [];
            
            foreach (var range in ranges)
            {
                (ulong start, ulong end) result = (0, 0);
                if (!ranges.Any(r => CheckRanges(r, range, out result))) continue;
                toRemove.Add(range);
                toRemove.Add(result);
                didMerge = true;
                break;
            }

            if (toRemove.Count != 0)
            {
                foreach (var r in toRemove)
                    ranges.Remove(r);
                
                (ulong start, ulong end) merged = (Math.Min(toRemove[0].start, toRemove[1].start), Math.Max(toRemove[0].end, toRemove[1].end));
                ranges.Add(merged);
            }
        }

        ulong freshableCount = ranges.Aggregate<(ulong start, ulong end), ulong>(0, (current, range) => current + (range.end - range.start + 1));
        return new ValueTask<string>(freshableCount.ToString());

        bool CheckRanges((ulong start, ulong end) a, (ulong start, ulong end) b, out (ulong start, ulong end) result)
        {
            result = a;
            return a != b && DoesOverlap(a.start, b.start, a.end, b.end);
        }
    }

    private static (ulong start, ulong end) ParseRange(string range)
    {
        ulong[] ends = range.Split('-').Select(ulong.Parse).ToArray();
        return (ends[0], ends[1]);
    }

    private static bool CheckInRange(ulong start, ulong end, ulong value) => (value >= start && value <= end);

    private static bool DoesOverlap(ulong startA, ulong startB, ulong endA, ulong endB) => startA < endB && startB < endA;
}