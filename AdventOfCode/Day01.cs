using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;

//[RunTest]
public sealed class Day01 : TestableDay
{
    private readonly string _input;
    private readonly int[] _dirs;

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath);
        _dirs = _input.Split("\n").Select(s => s.Replace('L', '-').Replace("R", null)).Select(int.Parse).ToArray();
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
        /*int runningSum = 50;
        int timesZero = 0;
        foreach (int dir in _dirs)
        {
            runningSum = Wrap(runningSum, dir, 100, out int timesWrapped);
            timesZero += timesWrapped;
            if (runningSum == 0)
                timesZero++;
        }*/
        int timesZero = SolveBad();
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

    private static int Wrap(int initX, int change, int max, out int timesWrapped)
    {
        int x = initX + change;
        timesWrapped = x / max;
        int wrapped = x % max;

        if (wrapped < 0)
        {
            wrapped += max;
            timesWrapped--;
        }

        timesWrapped = Math.Abs(timesWrapped);
        if ((initX == 0 || wrapped == 0) && timesWrapped > 0)
            timesWrapped--;
        return wrapped;
    }

    private int SolveBad()
    {
        int currentValue = 50;
        int timesZero = 0;
        foreach (int dir in _dirs)
        {
            if (dir >= 0)
                for (int i = 0; i < dir; i++)
                {
                    currentValue++;
                    if (currentValue < 0)
                        currentValue = 99;
                    if (currentValue > 99)
                        currentValue = 0;
                    if (currentValue == 0)
                        timesZero++;
                }
            else
                for (int i = 0; i < -dir; i++)
                {
                    currentValue--;
                    if (currentValue < 0)
                        currentValue = 99;
                    if (currentValue > 99)
                        currentValue = 0;
                    if (currentValue == 0)
                        timesZero++;
                }
        }
        return timesZero;
    }
}