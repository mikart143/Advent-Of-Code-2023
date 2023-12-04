using System.Text.RegularExpressions;

var input = await File.ReadAllTextAsync("input.txt");

var result = input
    .Split('\n')
    .Select(line => line.Split(':')[1])
    .Select(Deserialize)
    .Select(game => game.WinningNumbers.Intersect(game.Draws).Count())
    .Where(matches => matches > 0)
    .Select(matches => Math.Pow(2, matches - 1))
    .Sum();

Console.WriteLine(result);

static Game Deserialize(string line)
{
    var split = line.Split("|");
    var draws = NumberRegex()
        .Matches(split[0])
        .Select(match => int.Parse(match.Value))
        .ToList();

    var winningNumbers = NumberRegex()
        .Matches(split[1])
        .Select(match => int.Parse(match.Value))
        .ToList();

    return new(draws, winningNumbers);
}

public record Game(ICollection<int> Draws, ICollection<int> WinningNumbers);


partial class Program
{
    [GeneratedRegex("[0-9]+")]
    private static partial Regex NumberRegex();
}