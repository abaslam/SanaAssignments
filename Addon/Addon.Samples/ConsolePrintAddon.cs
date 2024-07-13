namespace Addon.Samples
{
    using Addon.Core;

    public class ConsolePrintAddon : IPrintTextAddon
    {
        public void Print(string text)
        {
            Console.WriteLine(text);
        }
    }
}
