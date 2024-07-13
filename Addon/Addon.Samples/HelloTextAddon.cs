namespace Addon.Samples
{
    using Addon.Core;
    public class HelloTextAddon : IProvideTextAddon
    {
        public string Get()
        {
            return "Hello";
        }
    }
}
