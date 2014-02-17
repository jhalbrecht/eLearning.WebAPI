using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Win8.Model
{
    //public class ELearning
    //{
    public class Tutor
    {
        public int id { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int gender { get; set; }
    }

    public class Subject
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    // [JsonObjectAttribute]

    // public class Result
    public class ApiResult
    {
        public int id { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        
        // public int duration { get; set; }
        // TODO why is the Json int being deserialized to decimal?
        public double duration { get; set; }
        public string description { get; set; }
        public Tutor tutor { get; set; }
        public Subject subject { get; set; }
    }

    public class RootObject
    {
        public int totalCount { get; set; }
        public int totalPages { get; set; }
        public string prevPageLink { get; set; }
        public string nextPageLink { get; set; }
        public List<ApiResult> results { get; set; }
    }

}

