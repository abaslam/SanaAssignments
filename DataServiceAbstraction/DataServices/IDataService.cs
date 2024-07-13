namespace DataServiceAbstraction.DataServices
{
    public interface IDataService
    {
        IEnumerable<string> GetLines();
    }
}
