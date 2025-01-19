using FunRun.MathGame.Gamemodes;
using FunRun.MathGame.Model;
using Spectre.Console;

namespace FunRun.MathGame;

public class GameSelection
{
    private Game _game;
    private Highscore _highscore;
    public GameSelection(Game game,Highscore highscore)
    {
        _game = game;
        _highscore = highscore; 
        
    }
    public async Task RunGame()
    {
        while (true)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new FigletText("Console.MathGame").Centered().Color(Color.Blue));

            AnsiConsole.MarkupLine("[blue] A minigame inpired by the [link=https://thecsharpacademy.com/project/53/math-game.net]C#Acadamy [/][/]");
            AnsiConsole.MarkupLine("");

            var gameModes = Enum.GetValues<GameMode>()
                .Select(g => g.ToString());

            var specialChoices = new[] { "Highscore", "Exit" };

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Select an option: [/]")
                    .PageSize(10)
                    .AddChoiceGroup("Game Modes:", gameModes) 
                    .AddChoices(specialChoices) 
            );

            if (selection == "Exit")
            {
                return;
            }
            else if (selection == "Highscore")
            {
                await _highscore.Run();
            }
            else
            {
                var selectedGameMode = Enum.Parse<GameMode>(selection);
                await _game.Run(selectedGameMode);
            }
        
        }

    }
}
