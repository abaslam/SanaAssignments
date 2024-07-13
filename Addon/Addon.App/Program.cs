using Addon.Core;
using System.Reflection;

var addonsPath = Path.Combine(AppContext.BaseDirectory, "addons");
if (!Directory.Exists(addonsPath))
{
    Console.WriteLine($"Addons folder '{addonsPath}' does not exist.");
    return;
}

var addonFiles = Directory.GetFiles(addonsPath, "*.dll");
var addons = addonFiles.SelectMany(file =>
{
    var assembly = Assembly.LoadFrom(file);
    return assembly.GetTypes()
        .Where(type => typeof(IAddon).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
        .Select(type => (IAddon?)Activator.CreateInstance(type));
}).ToList();

var textProviders = addons.OfType<IProvideTextAddon>().ToList();
var textPrinters = addons.OfType<IPrintTextAddon>().ToList();

foreach (var textProvider in textProviders)
{
    var text = textProvider.Get();

    foreach (var textPrinter in textPrinters)
    {
        textPrinter.Print(text);
    }
}



Console.ReadLine();
