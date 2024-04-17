using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories
{
    public class MyCoursesReporsitory
    {
        private readonly UserContext _userContext;
        private readonly DataContext _dataContext;

        public MyCoursesReporsitory(UserContext userContext, DataContext dataContext)
        {
            _userContext = userContext;
            _dataContext = dataContext;
        }

        public async Task<List<CourseEntity>> GetCourses(string userId)
        {
            try
            {
                var userCoursesList = _userContext.UserCourses.Where(x => x.UserId == userId).ToList();

                if (userCoursesList.Any() && userCoursesList != null)
                {
                    var courseList = new List<CourseEntity>();

                    if(userCoursesList != null)
                    {                       
                            foreach(var course in userCoursesList)
                            {
                                var userCourse = await _dataContext.Courses.FirstOrDefaultAsync(x => x.Id == course.CourseId);

                                if(userCourse != null)
                                {
                                    courseList.Add(userCourse);
                                }
                            }
                            return courseList;
                    }
                    return null!;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null!;
        }

        public async Task<UserEntity> AddCourse(int courseId, string userId)
        {
            try
            {
                var user = await _userContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

                if (user != null)
                {
                    if (user.Courses == null)
                    {
                        user.Courses = new List<UserCourseItemEntity>();
                    }

                    var userCourse = new UserCourseItemEntity
                    {
                        UserId = user.Id,
                        CourseId = courseId
                    };

                    user.Courses.Add(userCourse);
                    await _userContext.SaveChangesAsync();

                    return user;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null!;
        }

        public async Task<bool> DeleteACourse(int courseId, string userId)
        {
            try
            {
                var userCourses = _userContext.UserCourses.Where(x => x.UserId == userId).ToList();

                if (userCourses.Count != 0)
                {
                    foreach (var course in userCourses)
                    {
                        var courseToDelete = userCourses.FirstOrDefault(x => x.CourseId == courseId);

                        if (courseToDelete.CourseId == courseId)
                        {
                            _userContext.UserCourses.Remove(courseToDelete);
                            await _userContext.SaveChangesAsync();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task<bool> DeleteAllCourses(string userId)
        {
            try
            {
                var userCourses = _userContext.UserCourses.Where(x => x.UserId == userId).ToList();

                if (userCourses.Count != 0)
                {
                   _userContext.UserCourses.RemoveRange(userCourses);
                   await _userContext.SaveChangesAsync();
                   return true;
                }     
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task<bool> CourseExists(string userId, int courseId)
        {
            try
            {
                var user = _userContext.UserCourses.Where(x => x.UserId == userId).ToList();

                if (user != null && user.Count() != 0)
                {
                    foreach (var course in user)
                    {
                        if (courseId == course.CourseId)
                        {  return true; }
                    }                   
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
