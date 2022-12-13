using EntityFramework.BLL.Specifications;
using EntityFramework.DAL.Data;
using EntityFramework.DAL.Models;
using EntityFramework.DAL.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Tests.Repositories;

public class MedicalExaminationRepositoryTests
{
    private async Task<DatabaseContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var databaseContext = new DatabaseContext(options);
        await databaseContext.Database.EnsureCreatedAsync();
        if (await databaseContext.MedicalExaminations.CountAsync() <= 0)
        {
            for (int i = 0; i < 10; i++)
            {
                databaseContext.MedicalExaminations.Add(
                    new MedicalExamination()
                    {
                        Description = "Test",
                        Title = "Test",
                    }
                );
                await databaseContext.SaveChangesAsync();
            }
        }

        return databaseContext;
    }

    [Fact]
    public async void MedicalExaminationRepository_ListAsync_ReturnsMedicalExaminations()
    {
        //Arrange
        var paginationSpecification = new PaginationSpecificationParams()
        {
            PageIndex = 1,
            PageSize = 1,
            Sort = "titleAsc",
        };
        var dbContext = await GetDatabaseContext();
        var specification = new MedicalExaminationsWithAppointmentSpecification(paginationSpecification);
        var medicalExaminationRepository = new GenericRepository<MedicalExamination>(dbContext);

        //Act
        var result = medicalExaminationRepository.ListAsync(specification);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<IReadOnlyList<MedicalExamination>>));
    }

    [Fact]
    public async void MedicalExaminationRepository_ListAllSync_ReturnsMedicalExaminations()
    {
        //Arrange
        var dbContext = await GetDatabaseContext();
        var medicalExaminationRepository = new GenericRepository<MedicalExamination>(dbContext);

        //Act
        var result = medicalExaminationRepository.ListAllAsync();
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<IReadOnlyList<MedicalExamination>>));
    }
    
    [Fact]
    public async void MedicalExaminationRepository_CountAsync_ReturnsInt()
    {
        //Arrange
        var paginationSpecification = new PaginationSpecificationParams()
        {
            PageIndex = 1,
            PageSize = 1,
            Sort = "startDateAsc",
        };
        var specification = new MedicalExaminationsWithAppointmentSpecification(paginationSpecification);
        var dbContext = await GetDatabaseContext();
        var medicalExaminationRepository = new GenericRepository<MedicalExamination>(dbContext);

        //Act
        var result = medicalExaminationRepository.CountAsync(specification);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<int>));
    }

    [Theory]
    [InlineData(1)]
    public async void MedicalExaminationRepository_GetEntityWithSpec_ReturnsAppointment(int id)
    {
        //Arrange 
        var specification = new MedicalExaminationsWithAppointmentSpecification(id);
        var dbContext = await GetDatabaseContext();
        var medicalExaminationRepository = new GenericRepository<MedicalExamination>(dbContext);

        //Act
        var result = medicalExaminationRepository.GetEntityWithSpec(specification);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<MedicalExamination>));
    }

    [Fact]
    public async void MedicalExaminationRepository_Add_ReturnsInt()
    {
        //Arrange
        var entity = new MedicalExamination()
        {
            Description = "Add Test",
            Title = "Add Test",
        };
        var dbContext = await GetDatabaseContext();
        var medicalExaminationRepository = new GenericRepository<MedicalExamination>(dbContext);

        //Act
        medicalExaminationRepository.Add(entity);
        var result = await dbContext.SaveChangesAsync();

        //Assert
        result.Should().Be(1);
        result.Should().BeOfType(typeof(int));
    }


    [Fact]
    public async void MedicalExaminationRepository_Update_ReturnsInt()
    {
        //Arrange
        var dbContext = await GetDatabaseContext();
        var medicalExaminationRepository = new GenericRepository<MedicalExamination>(dbContext);
        var entity = await medicalExaminationRepository.GetByIdAsync(10);

        //Act
        medicalExaminationRepository.Update(entity);
        var result = await dbContext.SaveChangesAsync();

        //Assert
        result.Should().Be(1);
        result.Should().BeOfType(typeof(int));
    }

    [Fact]
    public async void MedicalExaminationRepository_Remove_ReturnsInt()
    {
        //Arrange
        var dbContext = await GetDatabaseContext();
        var medicalExaminationRepository = new GenericRepository<MedicalExamination>(dbContext);
        var entity = await medicalExaminationRepository.GetByIdAsync(10);

        //Act
        medicalExaminationRepository.Delete(entity);
        var result = await dbContext.SaveChangesAsync();

        //Assert
        result.Should().Be(1);
        result.Should().BeOfType(typeof(int));
    }
}