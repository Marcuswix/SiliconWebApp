using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SiliconTests.ApiTest
{
    //public class CourseRepositoryTests
    //{
    //    private readonly DataContext _dataContext = new DataContext(new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

    //    //Create
    //    [Fact]
    //    public async Task CreateOne_ShouldCreateACourse_ThenReturnARepositoriesResult()
    //    {

    //        //Arrange
    //        var _courseRepository = new CoursesRepository(_dataContext);
    //        var courseModel = new CourseModel { Author = "oklart", Title = "Test" };


    //        //Act
    //        var courseCreated = await _courseRepository.CreateOne(courseModel);

    //        //Assert
    //        Assert.NotNull(courseCreated);
    //    }

    //    //Get
    //    [Fact]
    //    public async Task CourseGetAll_ShouldGetALLCourses_ThenReturnARepositoriesResult()
    //    {

    //        //Arrange
    //        var _courseRepository = new CoursesRepository(_dataContext);
    //        var courseModel = new CourseModel { Author = "oklart", Title = "Test" };
    //        await _courseRepository.CreateOne(courseModel);


    //        //Act
    //        var result = await _courseRepository.GetAll();

    //        //Assert
    //        Assert.NotNull(result);
    ////    }
    //}
}