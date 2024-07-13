
namespace DataServiceAbstraction.DataServices
{
    public class FileDataService : IDataService
    {
        private readonly string filePath;

        public FileDataService(string filePath)
        {
            this.filePath = filePath;
        }

        public IEnumerable<string> GetLines()
        {
            return File.ReadAllLines(this.filePath);
        }
    }
}
