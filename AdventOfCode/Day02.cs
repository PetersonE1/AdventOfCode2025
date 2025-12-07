using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode;

//[RunTest]
public sealed partial class Day02 : TestableDay
{
    private readonly ulong[][] _ranges;

    public Day02()
    {
        string input = File.ReadAllText(InputFilePath);
        _ranges = input.Split(',').Select(s => s.Split('-').Select(ulong.Parse).ToArray()).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        ulong invalidIds = 0;
        foreach (ulong[] range in _ranges)
        {
            for (ulong i = range[0]; i <= range[1]; i++)
            {
                string rep = i.ToString();
                if (rep.Length % 2 != 0)
                    continue;
                
                string firstHalf = rep[..(rep.Length / 2)];
                string secondHalf = rep[(rep.Length / 2)..];
                if (firstHalf == secondHalf)
                    invalidIds += i;
            }
        }
        return new ValueTask<string>(invalidIds.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        ulong invalidIds = 0;
        foreach (ulong[] range in _ranges)
        {
            for (ulong i = range[0]; i <= range[1]; i++)
            {
                string rep = i.ToString();
                Regex regex = PatternRegex();
                if (regex.IsMatch(rep))
                    invalidIds += i;
            }
        }
        return new ValueTask<string>(invalidIds.ToString());
    }

    [GeneratedRegex(@"^(.+)(?:\1)+$")]
    private static partial Regex PatternRegex();
}