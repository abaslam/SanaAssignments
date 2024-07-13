namespace Addon.Samples
{
    using Addon.Core;   

    public class FilePrintAddon : IPrintTextAddon
    {
        public void Print(string text)
        {
            File.AppendAllLines("./output.txt", [text]);
        }
    }
}
