using Infrastructure.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public class CourseResponse
    {
        [JsonProperty("$id")]
        public string Id { get; set; }

        public int StatusCode { get; set; }

        [JsonProperty("contentResult")]
        public List<Dictionary<string, CourseEntity>>? ContentResult { get; set; }
    }
}
