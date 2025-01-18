using FunRun.MathGame.Gamemodes;
using Spectre.Console;

namespace FunRun.MathGame;

public class GameSelection
{
    private Game _game;
    public GameSelection(Game game)
    {
        _game = game;
    }
    public async Task RunGame()
    {
        while (true)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new FigletText("Console.MathGame").Centered().Color(Color.Blue));

            AnsiConsole.MarkupLine("[blue] A minigame provided by the [link=https://thecsharpacademy.com/project/53/math-game.net]C#Acadamy [/][/]");
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

            if (selection == "Highscore")
            {
                //ShowHighscore(); 
                return;
            }

        
            var selectedGameMode = Enum.Parse<GameMode>(selection);
            await _game.Run(selectedGameMode);
        }

    }
}
