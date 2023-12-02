using System.Text.RegularExpressions;

var input = await File.ReadAllTextAsync("input.txt");

var games = input
    .Split('\n')
    .Select(DeserializeLine)
    .ToList();

var result = games
    .Select(GamePower)
    .Sum();

Console.WriteLine(result);


static int GamePower(Game game)
{
    var red = game.Draws.MaxBy(e => e.Red)!.Red;
    var green = game.Draws.MaxBy(e => e.Green)!.Green;
    var blue = game.Draws.MaxBy(e => e.Blue)!.Blue;

    return red * green * blue;
}

static Game DeserializeLine(string line)
{
    var split = line.Split(':');
    var id = int.Parse(NumberRegex().Match(split[0]).Value);
    var games = split[1]
        .Split(';')
        .Select(game => game.Trim())
        .Select(DeserializeGame)
        .ToList();

    return new Game(id, games);
}

static Draw DeserializeGame(string line)
{
    var red = 0;
    var green = 0;
    var blue = 0;

    foreach (var entry in line.Split(','))
    {
        var count = int.Parse(NumberRegex().Match(entry).Value);
        switch (TextRegex().Match(entry).Value)
        {
            case "red":  red = count;
                break;
            case "blue":  blue = count;
                break;
            case "green":  green = count;
                break;
        }
    }

    return new Draw(red, green, blue);
}

public record Draw(int Red, int Green, int Blue);
public record Game(int Id, ICollection<Draw> Draws);

partial class Program
{
    [GeneratedRegex("[0-9]+")]
    private static partial Regex NumberRegex();

    [GeneratedRegex("[a-zA-Z]+")]
    private static partial Regex TextRegex();
}