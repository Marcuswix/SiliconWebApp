using Infrastructure.Entities;
using Infrastructure.Models;

namespace Infrastructure.Factories
{
    public class CourseFactory
    {
        public static CourseModel Create(CourseEntity entity) 
        {
            try
            {
                var model = new CourseModel
                {
                    Title = entity.Title,
                    Author = entity.Author,
                    Description = entity.Description,
                    DiscountPrice = entity.DiscountPrice,
                    Hours = entity.Hours,
                    IsBestseller = entity.IsBestseller,
                    LikesInNumbers = entity.LikesInNumbers,
                    LikesInProcent = entity.LikesInProcent,
                    Price = entity.Price,
                    WhatYouLearn = entity.WhatYouLearn,
                    ImageALtText = entity.ImageALtText,
                    ImageUrl = entity.ImageUrl,
                    CategoryName = entity.Category.CategoryName ?? string.Empty,
                };
                return model;
            }
            catch { }
            return null!;
        }

        public static IEnumerable<CourseModel> Create(List<CourseEntity> entities) 
        {
            List<CourseModel> courses = new List<CourseModel>();

            try
            {
                foreach (var entity in entities) 
                {
                    courses.Add(Create(entity)); 
                }
                return courses;
            }
            catch { }
            return null!;
        }
    }
}
