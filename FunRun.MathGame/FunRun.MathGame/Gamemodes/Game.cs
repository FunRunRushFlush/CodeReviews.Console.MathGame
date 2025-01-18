
using FunRun.MathGame.Model;
using Spectre.Console;

namespace FunRun.MathGame.Gamemodes;

public class Game
{
    private Random _rng;
    public async Task Run(GameMode gameMode)
    {
        int digits = DifficultySelection();
        int seed = SelectRngSeed();

        GameState state = new GameState(gameMode, seed);

        AnsiConsole.MarkupLine($"[red]{state.GameMode} - {state.GameSeed} [/]");
        while (true)
        {
            var num = GeneratingVariables(digits, gameMode);


            GeneratingQuestion(num, gameMode);
            long solution = Utils.Calculate(num.a, num.b, gameMode);
            long userInput = GetUserInput();

            if (userInput != solution)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new FigletText("GAME").Centered().Color(Color.Red));
                AnsiConsole.Write(new FigletText("OVER!").Centered().Color(Color.Red));

                AnsiConsole.MarkupLine($"[red]HighScore : {state.CalcHistory.Count} [/]");


                Console.ReadKey(true);
                break;
            }

            state.CalcHistory.Add(new CalculationVariables(num.a, num.b, gameMode));

        }
    }

    private int DifficultySelection()
    {
        Dictionary<int, string> difficulties = new Dictionary<int, string>
            {
                { 1_00, "[green]Easy[/] - Numbers have up to 2 Digits" },
                { 1_0000, "[yellow]Medium[/] - Numbers have up to 4 Digits" },
                { 1_000000, "[red]Hard[/] - Numbers have up to 6 Digits"}

            };

        var selection = AnsiConsole.Prompt(
                new SelectionPrompt<KeyValuePair<int, string>>()
                    .Title("Wähle eine Option:")
                    .AddChoices(difficulties)
                    .UseConverter(x => x.Value));

        return selection.Key;
    }

    private long GetUserInput()
    {
        try
        {
            var userInput = Console.ReadLine()?.Trim();

            return long.Parse(userInput);
        }
        catch
        {
            AnsiConsole.MarkupLine("[red]Ooopss.. Somthing went wrong [/]");
            AnsiConsole.MarkupLine("[red]Are you sure u typed a number?[/]");
            AnsiConsole.MarkupLine("[red]Try again...[/]");

            return GetUserInput();
        }

    }

    private void GeneratingQuestion((long a, long b) num, GameMode gameMode)
    {
        string mathOperator = Utils.GetOperator(gameMode);
        AnsiConsole.MarkupLine("What ist the Solution for: ");
        AnsiConsole.MarkupLine($"\t {num.a} {mathOperator} {num.b}");

    }

    private (long a, long b) GeneratingVariables(int digits, GameMode gameMode)
    {
        long varA = _rng.NextInt64(digits);
        long varB = _rng.NextInt64(digits);

        if (gameMode == GameMode.Division)
        {
            //This Rule gets broken for higher difficulty level:
            //The divisions should result on INTEGERS ONLY and dividends should go from 0 to 100
            while (true)
            {
                if (varA % varB == 0 && varB != 0) break;

                varA = _rng.NextInt64(digits);
                varB = _rng.NextInt64(digits);
            }
        }

        return (varA, varB);

    }

    private int SelectRngSeed(int seed = 0)
    {
        if (seed == 0)
        {
            seed = Environment.TickCount;
        }
        _rng = new Random(seed);

        return seed;
    }

}
