using Infrastructure.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories
{
    public class CourseDetailsRepository
    {
        private readonly DataContext _dataContext;

        public CourseDetailsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
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
    }
}
