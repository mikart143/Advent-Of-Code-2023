using System.Text.RegularExpressions;

var input = await File.ReadAllTextAsync("input.txt");

var result = input
    .Split('\n')
    .Select(line => line.Trim())
    .Where(line => line != string.Empty)
    .Select(PrepLine)
    .Select(GetCode)
    .Where(line => line != string.Empty)
    .Select(int.Parse)
    .Sum();

Console.WriteLine(result);


static string GetCode(string line)
{
    var matches = NumberRegex().Matches(line);
    return matches.Count switch
    {
        0 => string.Empty,
        1 => $"{matches[0].Value}{matches[0].Value}",
        _ => $"{matches[0].Value}{matches[^1].Value}"
    };
}

static string PrepLine(string line)
{
    var replaced = line;

    do
    {
        line = replaced;

        var matched = WordRegex().Match(line);

        if (!matched.Success)
            break;

        var regex = new Regex(Regex.Escape(matched.Value));
        replaced = regex.Replace(line,WordNumberMap[matched.Value], 1);

    } while (line != replaced);

    return replaced;
}

partial class Program
{
    [GeneratedRegex("[0-9]")]
    private static partial Regex NumberRegex();

    [GeneratedRegex("(one|two|three|four|five|six|seven|eight|nine)")]
    private static partial Regex WordRegex();

    private static readonly IDictionary<string, string> WordNumberMap = new Dictionary<string, string>
    {
        ["one"] = "1",
        ["two"] = "2",
        ["three"] = "3",
        ["four"] = "4",
        ["five"] = "5",
        ["six"] = "6",
        ["seven"] = "7",
        ["eight"] = "8",
        ["nine"] = "9",
    };
}