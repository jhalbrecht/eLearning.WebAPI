using Learning.Win8.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Win8.Service
{
    public interface IELearningDataService
    {
        Task<ObservableCollection<ApiResult>> GetCoursesAsync();
        Task<bool> PostApiResultAsync(ApiResult poster);
    }
}
