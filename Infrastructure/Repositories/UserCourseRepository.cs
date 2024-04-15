
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories
{

    public class UserCourseRepository
    {
        private readonly DataContext _dataContext;
        private readonly UserContext _userContext;
        public UserCourseRepository(DataContext dataContext, UserContext userContext)
        {
            _dataContext = dataContext;
            _userContext = userContext;
        }

        public async Task<UserCourse> UserCourse(int courseId, string userId)
        {
            try
            {
                var user = await _userContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
                var course = await _dataContext.Courses.FirstOrDefaultAsync(x => x.Id == courseId);

                if (user != null && course != null)
                {
                    var userCourse = new UserCourse
                    {
                        Course = course,
                        User = user
                    };

                    var result = await _dataContext.AddAsync(userCourse);
                    await _dataContext.SaveChangesAsync();

                    if (result != null)
                    {
                        return userCourse;
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }


            return null!;
        }
    }
}
