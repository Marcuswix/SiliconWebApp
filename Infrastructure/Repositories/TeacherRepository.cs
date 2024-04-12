using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories
{
    public class TeacherRepository
    {
        private readonly DataContext _context;

        public TeacherRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<TeacherEntity> GetOneTeacher(int id)
        {
            try 
            {
                if(id != 0)
                {
                    var result = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == id);
                    if(result != null)
                    {
                        return result;
                    }
                }
                return null!;
            }
            catch (Exception ex){Debug.WriteLine(ex.Message);
                return null!;
            }
        }
    }
}
