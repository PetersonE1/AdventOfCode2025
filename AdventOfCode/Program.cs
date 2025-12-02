const string testInputFullPath = @"C:\Users\Peter\RiderProjects\AdventOfCode2025\AdventOfCode\TestInputs";

foreach (FileInfo file in new DirectoryInfo(testInputFullPath).GetFiles())
{
    file.CopyTo(Path.Combine(Directory.GetCurrentDirectory(), "TestInputs", file.Name), true);
}

if (args.Length == 0)
{
    await Solver.SolveLast(opt => opt.ClearConsole = false);
}
else if (args.Length == 1 && args[0].Contains("all", StringComparison.CurrentCultureIgnoreCase))
{
    await Solver.SolveAll(opt =>
    {
        opt.ShowConstructorElapsedTime = true;
        opt.ShowTotalElapsedTimePerDay = true;
    });
}
else
{
    var indexes = args.Select(arg => uint.TryParse(arg, out var index) ? index : uint.MaxValue);

    await Solver.Solve(indexes.Where(i => i < uint.MaxValue));
}
