using Spectre.Console;
using Microsoft.Extensions.DependencyInjection;
using FunRun.MathGame;
using FunRun.MathGame.Gamemodes;


var services = ConfigureServices();
var serviceProvider = services.BuildServiceProvider();

var solver = serviceProvider.GetRequiredService<GameSelection>();
await solver.RunGame();

static ServiceCollection ConfigureServices()
{
    var services = new ServiceCollection();

    services.AddSingleton<Game>();
    services.AddSingleton<Highscore>();
    services.AddSingleton<GameSelection>();


    return services;
}