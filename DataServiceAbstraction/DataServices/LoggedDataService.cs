namespace DataServiceAbstraction.DataServices
{
    using DataServiceAbstraction.Infrastructure.Logging;
    public class LoggedDataService : IDataService
    {
        private readonly IDataService dataService;
        private readonly ILogger logger;

        public LoggedDataService(IDataService dataService, ILogger logger)
        {
            this.dataService = dataService;
            this.logger = logger;
        }

        public IEnumerable<string> GetLines()
        {
            this.logger.Log("Fetching lines from data service.");
            var result = this.dataService.GetLines();
            this.logger.Log($"Retrieved {result.Count()} lines.");
            return result;
        }
    }
}
