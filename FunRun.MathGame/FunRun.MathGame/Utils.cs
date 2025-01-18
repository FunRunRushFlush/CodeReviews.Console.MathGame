namespace FunRun.MathGame
{
    public static class Utils
    {
        public static string GetOperator(GameMode mode)
        {
            return mode switch
            {
                GameMode.Addition => "+",
                GameMode.Subtraction => "-",
                GameMode.Multiplication => "*",
                GameMode.Division => "/",
                _ => throw new NotImplementedException()
            };
        }

        public static long Calculate(long a, long b, GameMode mode)
        {
            return mode switch
            {
                GameMode.Addition => a + b,
                GameMode.Subtraction => a - b,
                GameMode.Multiplication => a * b,
                GameMode.Division when b != 0 => a / b,
                GameMode.Division => throw new DivideByZeroException(),
                _ => throw new ArgumentException("Invalid GameMode")
            };
        }
    }
}
