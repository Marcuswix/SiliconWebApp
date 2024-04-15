using Infrastructure.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories
{
    public class CourseDetailsRepository
    {
        private readonly DataContext _dataContext;
        private readonly UserContext _userContext;

        public CourseDetailsRepository(DataContext dataContext, UserContext userContext)
        {
            _dataContext = dataContext;
            _userContext = userContext;
        }

        public async Task<ProgramDetailsEntity> GetProgramsDetails(int id)
        {

            var result = await _dataContext.ProgramDetails.Include(x => x.ProgramDetails).FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                return result;
            }

            return null!;
        }

        public async Task<WhatYouLearnEntity> GetWhatYouLearn(int id)
        {

            var result = await _dataContext.WhatYouLearn.Include(x => x.whatYouLearnItems).FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            { return result; }

            return null! ;

        }

        public async Task<UserEntity> AddCourseToUser(int courseId, string userId)
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

    }
}
