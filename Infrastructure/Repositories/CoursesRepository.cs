using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Repositories
{
    public class CoursesRepository
    {
        private readonly DataContext _dataContext;

        public CoursesRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<RepositoriesResult> GetAll()
        {
            try
            {
                var result = _dataContext.Courses.ToList();

                if(result != null)
                {
                    return ResponseFactory.Ok(result);
                }
                if(result == null)
                {
                    return ResponseFactory.NotFound();
                }
                return ResponseFactory.Error();

            }
            catch (Exception ex) 
            { Debug.WriteLine("GetAllCourses" + ex.Message);
                return ResponseFactory.Error();
            }
        }

        public RepositoriesResult GetOneById(int id)
        {
            try
            {
                var result = _dataContext.Courses.FirstOrDefault(x => x.Id == id);

                if (result != null)
                {
                    return ResponseFactory.Ok(result);
                }
                if (result == null)
                {
                    return ResponseFactory.NotFound();
                }
                return ResponseFactory.Error();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetOneCourses" + ex.Message);
                return ResponseFactory.Error();
            }
        }

        public virtual async Task<RepositoriesResult> GetOneAsync(Expression<Func<CourseEntity, bool>> predicate)
        {
            try
            {
                var entityToGet = await _dataContext.Courses.FirstOrDefaultAsync(predicate);

                if (entityToGet == null)
                {
                    return ResponseFactory.NotFound();
                }
                else
                {
                    return ResponseFactory.Ok(entityToGet);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetOneAsync" + ex.Message);
                return ResponseFactory.Error(ex.Message);
            }
        }

        public RepositoriesResult NameExist(string title)
        {
            try
            {
                var result = _dataContext.Courses.FirstOrDefault(x => x.Title == title);

                if (result == null)
                {
                    return ResponseFactory.Ok();
                }
                if (result != null)
                {
                    return ResponseFactory.AlreadyExist();
                }
                return ResponseFactory.Error();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetOneCourses" + ex.Message);
                return ResponseFactory.Error();
            }
        }



        public async Task<RepositoriesResult> CreateOne(CourseModel model)
{
    try
    {
        var entity = new CourseEntity
        {
            Title = model.Title,
            Author = model.Author,
            Description = model.Description,
            DiscountPrice = model.DiscountPrice,
            Hours = model.Hours,
            IsBestseller = model.IsBestseller,
            LikesInNumbers = model.LikesInNumbers,
            LikesInProcent = model.LikesInProcent,
            Price = model.Price,
            WhatYouLearn = model.WhatYouLearn,
        };

        var result = _dataContext.Courses.Add(entity);
        await _dataContext.SaveChangesAsync();

        if (entity.Id > 0 && result != null)
        {
            var course = new CourseModel
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
            };
            
            return ResponseFactory.Ok(course);
        }
        else
        {
            return ResponseFactory.NotFound();
        }
    }
    catch (Exception ex)
    {
        Debug.WriteLine("CreateOneCourses" + ex.Message);
        return ResponseFactory.Error();
    }
}


        public async Task<RepositoriesResult> Delete(int id)
        {
            try
            {
                var entityToDelete = await _dataContext.Courses.FirstOrDefaultAsync(x => x.Id == id);

                if (entityToDelete != null)
                {
                    var result = _dataContext.Courses.Remove(entityToDelete);
                    await _dataContext.SaveChangesAsync();
                    return ResponseFactory.Ok();
                }
                else
                { 
                    return ResponseFactory.NotFound();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DeleteOneCourse" + ex.Message);
                return ResponseFactory.Error();
            }
        }

        public async Task<RepositoriesResult> Update(int id, CourseModel model)
        {
            try
            {
                var courseToUpdate = await _dataContext.Courses.FirstOrDefaultAsync(x => x.Id == id);

                //if(courseToUpdate != null)
                //{
                //    courseToUpdate.Title = model.Title;
                //    courseToUpdate.Author = model.Author;
                //    courseToUpdate.Description = model.Description;
                //    courseToUpdate.DiscountPrice = model.DiscountPrice;
                //    courseToUpdate.Hours = model.Hours;
                //    courseToUpdate.IsBestseller = model.IsBestseller;
                //    courseToUpdate.LikesInNumbers = model.LikesInNumbers;
                //    courseToUpdate.LikesInProcent = model.LikesInProcent;
                //    courseToUpdate.Price = model.Price;
                //    courseToUpdate.WhatYouLearn = model.WhatYouLearn;
                //};
                if(courseToUpdate != null)
                {
                    _dataContext.Entry(courseToUpdate).CurrentValues.SetValues(model);
                    await _dataContext.SaveChangesAsync();
                    return ResponseFactory.Ok();
                }
                if(courseToUpdate == null)
                {
                    return ResponseFactory.NotFound();
                }
                else
                {
                    return ResponseFactory.Error();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UpdateCourse" + ex.Message);
                return ResponseFactory.Error();
            }
        }
    }
}
