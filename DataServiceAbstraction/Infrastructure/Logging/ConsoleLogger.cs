namespace DataServiceAbstraction.Infrastructure.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            var existingColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ForegroundColor = existingColor;
        }
    }
}
