namespace DataServiceAbstraction.Infrastructure.Caching
{
    public interface ICache
    {
        T? Get<T>(string key);
        T Set<T>(string key, T value);
    }
}
