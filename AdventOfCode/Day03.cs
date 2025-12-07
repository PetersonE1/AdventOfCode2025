using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;

//[RunTest]
public sealed class Day03 : TestableDay
{
    private readonly string _input;
    private readonly string[] _lines;

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
        _lines = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1() => ValueTask.FromResult(_lines.Aggregate<string, ulong>(0, (current, line) => current + CalculateJoltage(line, 2)).ToString());

    public override ValueTask<string> Solve_2() => ValueTask.FromResult(_lines.Aggregate<string, ulong>(0, (current, line) => current + CalculateJoltage(line, 12)).ToString());

    private static ulong CalculateJoltage(string input, int digits)
    {
        int leftPos = 0;
        StringBuilder sb = new();
        for (int digit = 1; digit <= digits; digit++)
        {
            int cachedHighest = 0;
            for (int i = leftPos; i < input.Length - (digits - digit); i++)
            {
                int current = int.Parse(input[i].ToString());
                if (current <= cachedHighest) continue;

                cachedHighest = current;
                leftPos = i + 1;
            }
            sb.Append(cachedHighest);
        }
        
        return ulong.Parse(sb.ToString());
    }
}