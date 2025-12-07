using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;

//[RunTest]
public sealed class Day01 : TestableDay
{
    private readonly int[] _dirs;

    public Day01()
    {
        string input = File.ReadAllText(InputFilePath);
        _dirs = input.Split("\n").Select(s => s.Replace('L', '-').Replace("R", null)).Select(int.Parse).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        int runningSum = 50;
        int timesZero = 0;
        foreach (int dir in _dirs)
        {
            runningSum = Wrap(runningSum + dir, 0, 100);
            if (runningSum == 0)
                timesZero++;
        }
        return new ValueTask<string>(timesZero.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int pos = 50;
        int timesZero = 0;
        foreach (int dist in _dirs)
        {
            if (dist > 0)
                timesZero += (int)(Math.Floor((double)(pos + dist) / 100) - Math.Floor((double)pos / 100));
            else
                timesZero += (int)(Math.Floor((double)(pos - 1) / 100) - Math.Floor((double)(pos + dist - 1) / 100));
            pos = Mod(pos + dist, 100);
        }
        return new ValueTask<string>(timesZero.ToString());
    }

    private static int Wrap(int x, int min, int max)
    {
        int range = max - min;
        if (range <= 0)
            return min;

        int relX = x - min;
        int wrapped = (relX % range + range) % range;

        return wrapped + min;
    }

    private static int Mod(int x, int m)
    {
        return (x % m + m) % m;
    }
}