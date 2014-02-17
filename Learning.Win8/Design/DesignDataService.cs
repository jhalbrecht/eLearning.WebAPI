using System.Threading.Tasks;
using Learning.Win8.Model;

namespace Learning.Win8.Design
{
    public class DesignDataService : IDataService
    {
        public Task<DataItem> GetData()
        {
            // Use this to create design time data

            var item = new DataItem("Welcome to MVVM Light [design]");
            return Task.FromResult(item);
        }
    }
}