namespace Addon.Samples
{
    using Addon.Core;

    public class WorldTextAdon : IProvideTextAddon
    {
        public string Get()
        {
            return "World";
        }
    }
}
