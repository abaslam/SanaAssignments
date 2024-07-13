using DataServiceAbstraction.Infrastructure.Caching;

namespace DataServiceAbstraction.DataServices
{
    public class CachedDataService : IDataService
    {
        private const string cacheKey = nameof(CachedDataService);
        private readonly IDataService dataService;
        private ICache cache;

        public CachedDataService(IDataService dataService, ICache cache)
        {
            this.dataService = dataService;
            this.cache = cache;
        }

        public IEnumerable<string> GetLines()
        {
            var data = this.cache.Get<IEnumerable<string>>(cacheKey);

            if (data == null)
            {
                data = this.cache.Set(cacheKey, this.dataService.GetLines());
            }

            return data;
        }
    }

}
