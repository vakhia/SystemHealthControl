using EntityFramework.BLL.Specifications;
using EntityFramework.DAL.Data;
using EntityFramework.DAL.Models;
using EntityFramework.DAL.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Tests.Repositories;

public class AppointmentRepositoryTests
{
    private async Task<DatabaseContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var databaseContext = new DatabaseContext(options);
        await databaseContext.Database.EnsureCreatedAsync();
        if (await databaseContext.Appointments.CountAsync() <= 0)
        {
            for (int i = 0; i < 10; i++)
            {
                databaseContext.Appointments.Add(
                    new Appointment()
                    {
                        DoctorId = 1,
                        ClientId = 1,
                        Description = "Test",
                        Title = "Test",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                    }
                );
                await databaseContext.SaveChangesAsync();
            }
        }
    
        return databaseContext;
    }
    
    [Fact]
    public async void AppointmentRepository_GetAppointments_ReturnsAppointments()
    {
        //Arrange
        var paginationSpecification = new PaginationSpecificationParams()
        {
            PageIndex = 1,
            PageSize = 1,
            Sort = "startDateAsc",
        };
        var dbContext = await GetDatabaseContext();
        var specification = new AppointmentsWithExaminationsSpecifications(paginationSpecification);
        var appointmentRepository = new GenericRepository<Appointment>(dbContext);
    
        //Act
        var result = appointmentRepository.ListAsync(specification);
        
    
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<IReadOnlyList<Appointment>>));
    }
}