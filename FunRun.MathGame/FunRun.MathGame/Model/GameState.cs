namespace FunRun.MathGame.Model;

public record GameState
{
    public GameState(GameMode gameMode, int gameSeed)
    {
        GameMode = gameMode;
        GameSeed = gameSeed;
    }

    public GameMode GameMode { get; set; }
    public int GameSeed { get; set; }
    public List<CalculationVariables> CalcHistory { get; set; } = new List<CalculationVariables>(); 

}

public struct CalculationVariables
{
    public long VarA { get; }
    public long VarB { get; }
    public long Solution { get; }
    public string Operator { get; }

    public CalculationVariables(long varA, long varB, GameMode gameMode)
    {
        VarA = varA;
        VarB = varB;
        Operator = Utils.GetOperator(gameMode);
        Solution = Utils.Calculate(varA,varB, gameMode);
    }
}

