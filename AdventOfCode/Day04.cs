using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;

//[RunTest]
public sealed class Day04 : TestableDay
{
    private readonly string _input;
    private readonly int _lineLength;
    private readonly int _lineCount;

    public Day04()
    {
        _input = File.ReadAllText(InputFilePath);
        _lineLength = _input.IndexOf('\r');
        _input = _input.ReplaceLineEndings("");
        _lineCount = _input.Length / _lineLength;
    }

    public override ValueTask<string> Solve_1()
    {
        ulong count = 0;
        
        for (int y = 0; y < _lineCount; y++)
            for (int x = 0; x < _lineLength; x++)
                if (GetCharAt(_input, x, y) == '@' && GetSurroundingCount(_input, x, y) < 4)
                    count++;
        
        return new ValueTask<string>(count.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        ulong count = 0;
        
        string input = _input;
        
        long cachedRemoved = -1;
        while (cachedRemoved != 0)
        {
            List<(int x, int y)> toRemove = [];
            
            for (int y = 0; y < _lineCount; y++)
            for (int x = 0; x < _lineLength; x++)
                if (GetCharAt(input, x, y) == '@' && GetSurroundingCount(input, x, y) < 4)
                    toRemove.Add((x, y));
            
            cachedRemoved = toRemove.Count;
            
            foreach ((int x, int y) loc in toRemove)
                SetCharAt(ref input, loc.x, loc.y, ".");
            count += (ulong)cachedRemoved;
        }
        
        return new ValueTask<string>(count.ToString());
    }

    private char GetCharAt(string input, int x, int y) => input[y * _lineLength + x];

    private void SetCharAt(ref string input, int x, int y, string value)
    {
        int index = y * _lineLength + x;
        input = input.Remove(index, 1);
        input = input.Insert(index, value);
    }

    private int GetSurroundingCount(string input, int x, int y)
    {
        int count = 0;
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i == x && j == y) continue;
                if (i < 0 || i >= _lineLength || j < 0 || j >= _lineCount) continue;
                if (GetCharAt(input, i, j) == '@')
                    count++;
            }
        }
        return count;
    }
}