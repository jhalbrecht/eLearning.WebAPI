using System.Net.Http.Headers;
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
        private string webApiUrl;
        private UriBuilder objUri;
        public ELearningDataService()
        {
            _logger = new Logger();
            _logger.Log(this, "ELearningDataService()\n");
            //Courses = new ObservableCollection<RootObject>();
            //LoadData();

            objUri = new UriBuilder {Scheme = "http", Port = 8323, Host = "localhost", Path = "api/courses"};
            webApiUrl = objUri.ToString();
        }

        private async void LoadData()
        {
            Courses = await GetCoursesAsync();
        }

        private ObservableCollection<ApiResult> Courses;

        public async Task<ObservableCollection<ApiResult>> GetCoursesAsync()
        {


            _logger.Log(this, "objUri", objUri.ToString());

            try
            {
                var client = new HttpClient();
                using (var response = await client.GetAsync(webApiUrl))
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

        public async Task<bool> PostApiResultAsync(ApiResult poster)
        {
            _logger.Log(this, "post em baby!");
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(webApiUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var req = new HttpRequestMessage(HttpMethod.Post, webApiUrl);
                req.Content = new StringContent(JsonConvert.SerializeObject(poster), Encoding.UTF8, "application/json");
                await client.SendAsync(req).ContinueWith(respTask =>
                    {
                        _logger.Log(this, "response result", respTask.Result.ToString());
                    });
                return true;
            }
            catch (Exception e)
            {
                _logger.Log(this, "threw exception", e.ToString());
                return false;
            }
        }
    }
}
