using FunRun.MathGame.Model;
using Spectre.Console;

namespace FunRun.MathGame.Gamemodes;

public class Highscore
{
    public async Task Run()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("Highscore").Centered().Color(Color.Blue));


        var selection = AnsiConsole.Prompt(
    new SelectionPrompt<GameState>()
        .Title("[yellow]Select an option: [/]")
        .PageSize(10)
        .AddChoices(GameHistory.GameStates)
        .UseConverter(x => $"Mode: {x.GameMode} - Score: {x.CalcHistory.Count}")
);

        foreach (var sel in selection.CalcHistory)
        {
            AnsiConsole.MarkupLine($"Calc: {sel.VarA} {sel.Operator} {sel.VarB} =  {sel.Solution}");
        }

        Console.ReadKey(true);

    }
}
