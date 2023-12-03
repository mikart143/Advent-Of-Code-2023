using System.Text.RegularExpressions;

var input = await File.ReadAllTextAsync("input.txt");
var lines = input.Split('\n');

var toSum = new List<int>();

for (int i = 0; i < lines.Length; i++)
{
    var topSymbols = new List<Match>();
    var bottomSymbols = new List<Match>();
    var lineSymbols = new List<Match>();

    if (i - 1 >= 0)
    {
        topSymbols.AddRange(SymbolRegex().Matches(lines[i - 1]));
    }

    if (i + 1 < lines.Length)
    {
        bottomSymbols.AddRange(SymbolRegex().Matches(lines[i + 1]));
    }

    lineSymbols.AddRange(SymbolRegex().Matches(lines[i]));

    foreach (var match in NumberRegex().Matches(lines[i]).ToList())
    {
        if(topSymbols.Count == 0 && bottomSymbols.Count == 0 && lineSymbols.Count == 0)
            continue;

        var xMin = match.Index - 1 >= 0 ? match.Index - 1 : 0;
        var xMax = match.Index + match.Length < lines[i].Length ? match.Index + match.Length : lines[i].Length - 1;
        var xRange = Enumerable.Range(xMin, xMax - xMin + 1);

        if (
            topSymbols.Exists(m => xRange.Contains(m.Index)) ||
            bottomSymbols.Exists(m => xRange.Contains(m.Index)) ||
            lineSymbols.Exists(m => xRange.Contains(m.Index))
            )
        {
            toSum.Add(int.Parse(match.Value));
        }

    }
}

Console.WriteLine(toSum.Sum());


partial class Program
{
    [GeneratedRegex("[0-9]+")]
    private static partial Regex NumberRegex();

    [GeneratedRegex("[^0-9.]")]
    private static partial Regex SymbolRegex();
}