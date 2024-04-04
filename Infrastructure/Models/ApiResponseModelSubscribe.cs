using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class ApiResponseModel
    {
        public int StatusCode { get; set; }
        public List<SubscriberEntity>? ContentResult { get; set; }
        public string? Message { get; set; }
    }

}
