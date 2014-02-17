using System.Threading.Tasks;

namespace Learning.Win8.Model
{
    public interface IDataService
    {
        Task<DataItem> GetData();
    }
}