using System.Text.RegularExpressions;

var input = await File.ReadAllTextAsync("input.txt");
var lines = input.Split('\n');

var toSum = new List<int>();

for (int i = 0; i < lines.Length; i++)
{
    var topNumbers = new List<Match>();
    var bottomNumbers = new List<Match>();
    var lineNumbers = new List<Match>();fo

    if (i - 1 >= 0)
    {
        topNumbers.AddRange(NumberRegex().Matches(lines[i - 1]));
    }

    if (i + 1 < lines.Length)
    {
        bottomNumbers.AddRange(NumberRegex().Matches(lines[i + 1]));
    }

    lineNumbers.AddRange(NumberRegex().Matches(lines[i]));


    foreach (var asteriskMatch in SymbolRegex().Matches(lines[i]).ToList())
    {
        if (topNumbers.Count == 0 && bottomNumbers.Count == 0 && lineNumbers.Count == 0)
            continue;

        var xAsteriskRange = MakeRange(asteriskMatch, lines, i);

        var multiplication = new List<int>();

        multiplication.AddRange(topNumbers.Where(m => MakeRange(m, lines, i).Skip(1).SkipLast(1).Intersect(xAsteriskRange).Any()).Select(m => int.Parse(m.Value)));
        multiplication.AddRange(bottomNumbers.Where(m => MakeRange(m, lines, i).Skip(1).SkipLast(1).Intersect(xAsteriskRange).Any()).Select(m => int.Parse(m.Value)));
        multiplication.AddRange(lineNumbers.Where(m => MakeRange(m, lines, i).Skip(1).SkipLast(1).Intersect(xAsteriskRange).Any()).Select(m => int.Parse(m.Value)));

        if(multiplication.Any() && multiplication.Count > 1 )
            toSum.Add(multiplication.Aggregate(1, (a, b) => a * b));
    }
}

Console.WriteLine(toSum.Sum());


static ICollection<int> MakeRange(Match match, string[] lines, int i)
{
    var xMin = match.Index - 1 >= 0 ? match.Index - 1 : 0;
    var xMax = match.Index + match.Length < lines[i].Length ? match.Index + match.Length : lines[i].Length - 1;
    return Enumerable.Range(xMin, xMax - xMin + 1).ToList();
}

partial class Program
{
    [GeneratedRegex("[0-9]+")]
    private static partial Regex NumberRegex();

    [GeneratedRegex("[*]")]
    private static partial Regex SymbolRegex();
}