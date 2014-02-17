using AppDevPro.Utility;
using Learning.Win8.Model;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Learning.Win8.Service
{
    public class ELearningDataService : IELearningDataService
    {
        private Logger _logger;

        public ELearningDataService()
        {
            _logger = new Logger();
            _logger.Log(this, "ELearningDataService()\n");
            //Courses = new ObservableCollection<RootObject>();
            //LoadData();
        }

        private async void LoadData()
        {
            Courses = await GetCoursesAsync();
        }

        private ObservableCollection<ApiResult> Courses;

        public async Task<ObservableCollection<ApiResult>> GetCoursesAsync()
        {
            UriBuilder objUri = new UriBuilder();
            objUri.Scheme = "http";
            objUri.Port = 8323;
            objUri.Host = "localhost";
            objUri.Path = "api/courses";

            _logger.Log(this, "objUri", objUri.ToString());

            try
            {
                var client = new HttpClient();
                using (var response = await client.GetAsync(objUri.ToString()))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        // JObject o = JObject.Parse(data);

                        var coursesResult = JsonConvert.DeserializeObject<RootObject>(data);
                        _logger.Log(this, "private async Task<ObservableCollection<CourseResult>> GetRemoteSoundsAsync()");
                        return new ObservableCollection<ApiResult>(coursesResult.results);
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.Log(this, "GetEsfxesAsync threw exception: ", e.ToString());
                return null;
            }
        }
    }
}
