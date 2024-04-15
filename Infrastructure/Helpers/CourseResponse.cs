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
        public CourseEntity ContentResult { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
