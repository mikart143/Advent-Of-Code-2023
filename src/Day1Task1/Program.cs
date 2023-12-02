using System.Text.RegularExpressions;

var input = await File.ReadAllTextAsync("input.txt");

var regex = MyRegex();

var result = input
    .Split('\n')
    .Select(line => line.Trim())
    .Where(line => line != string.Empty)
    .Select(line => GetCode(line, regex))
    .Where(line => line != string.Empty)
    .Select(int.Parse)
    .Sum();

Console.WriteLine(result);


static string GetCode(string line, Regex regex)
{
    var matches = regex.Matches(line);
    return matches.Count switch
    {
        0 => string.Empty,
        1 => $"{matches[0].Value}{matches[0].Value}",
        _ => $"{matches[0].Value}{matches[^1].Value}"
    };
}

partial class Program
{
    [GeneratedRegex("[0-9]")]
    private static partial Regex MyRegex();
}