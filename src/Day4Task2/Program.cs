using System.Text.RegularExpressions;

var input = await File.ReadAllTextAsync("input.txt");

var lines = input
    .Split('\n');

var games = lines
    .Select(line => line.Split(':')[1])
    .Select(Deserialize)
    .ToList();

var result = new List<int?>();

// Iteration over original games
for (int i = 0; i < games.Count; i++)
{
    if(result.ElementAtOrDefault(i) is null)
        result.Add(0);

    result[i] += 1;

    var wins = games[i].WinningNumbers.Intersect(games[i].Draws).Count();

    if(wins == 0)
        continue;
    // Each copy of card
    for (int k = 0; k < result[i]; k++)
    {
        // Adding copies furter
        for (int j = 1; j <= wins ; j++)
        {
            if(result.ElementAtOrDefault(i+j) is null)
                result.Add(0);

            result[i+j] += 1;
        }
    }
}


Console.WriteLine(result.Sum());



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