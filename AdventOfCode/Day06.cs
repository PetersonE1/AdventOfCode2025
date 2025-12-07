using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;

//[RunTest]
public sealed class Day06 : TestableDay
{
    private readonly string _input;
    private readonly string[][] _lines;

    public Day06()
    {
        _input = File.ReadAllText(InputFilePath);
        _lines = File.ReadAllLines(InputFilePath).Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        ulong result = 0;
        
        for (int problemNum = 0; problemNum < _lines[0].Length; problemNum++)
        {
            string op = _lines[^1][problemNum];
            switch (op)
            {
                case "+":
                {
                    ulong runningSum = 0;
                    for (int num = 0; num < _lines.Length - 1; num++)
                    {
                        runningSum += ulong.Parse(_lines[num][problemNum]);
                    }
                    result += runningSum;
                    break;
                }
                case "*":
                {
                    ulong runningProduct = 1;
                    for (int num = 0; num < _lines.Length - 1; num++)
                    {
                        runningProduct *= ulong.Parse(_lines[num][problemNum]);
                    }
                    result += runningProduct;
                    break;
                }
                default: throw new InvalidOperationException("Unknown op: " + op);
            }
        }
        
        return new ValueTask<string>(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        string input = _input.ReplaceLineEndings("");
        int lineLength = _input.IndexOf('\r');
        int lines = input.Length / lineLength;

        long result = 0;
        
        List<long> numbers = [];
        for (int x = lineLength - 1; x >= 0; x--)
        {
            StringBuilder sb = new();
            for (int y = 0; y < lines - 1; y++)
            {
                sb.Append(GetCharAt(input, lineLength, x, y));
            }

            string numString = sb.ToString().Trim();
            if (numString.Length != 0)
                numbers.Add(long.Parse(numString));
            switch (GetCharAt(input, lineLength, x, lines - 1))
            {
                case '+':
                {
                    result += numbers.Sum();
                    numbers.Clear();
                    break;
                }
                case '*':
                {
                    result += numbers.Aggregate(1L, (current, number) => current * number);
                    numbers.Clear();
                    break;
                }
                case ' ': break;
                default: throw new InvalidOperationException("Unknown op: " + GetCharAt(input, lineLength, x, lines - 1));
            }
        }
        
        return new ValueTask<string>(result.ToString());
    }
    
    private char GetCharAt(string input, int lineLength, int x, int y) => input[y * lineLength + x];
}