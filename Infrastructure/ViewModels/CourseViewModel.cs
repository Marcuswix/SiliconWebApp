using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class CourseViewModel
    {
        public List<CourseEntity>? Courses { get; set; }

        public List<CategoryEntity>? Categories { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; } 
    }
}
