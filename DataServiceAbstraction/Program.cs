
using DataServiceAbstraction.DataServices;
using DataServiceAbstraction.Infrastructure.Caching;
using DataServiceAbstraction.Infrastructure.Logging;

var consoleLogger = new ConsoleLogger();
var inMemoryCache = new InMemoryCache();

var fileDataService = new FileDataService("./Resources/data.txt");
var cachedDataService = new CachedDataService(fileDataService, inMemoryCache);
var loggedDataService = new LoggedDataService(cachedDataService, consoleLogger);

var result = loggedDataService.GetLines();

foreach (var line in result)
{
    Console.WriteLine(line);
}
