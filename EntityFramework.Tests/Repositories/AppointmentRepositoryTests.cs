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
    public async void AppointmentRepository_ListAsync_ReturnsAppointments()
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
    
    [Fact]
    public async void AppointmentRepository_ListAllSync_ReturnsAppointments()
    {
        //Arrange
        var dbContext = await GetDatabaseContext();
        var appointmentRepository = new GenericRepository<Appointment>(dbContext);

        //Act
        var result = appointmentRepository.ListAllAsync();
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<IReadOnlyList<Appointment>>));
    }

    [Fact]
    public async void AppointmentRepository_CountAsync_ReturnInt()
    {
        //Arrange
        var paginationSpecification = new PaginationSpecificationParams()
        {
            PageIndex = 1,
            PageSize = 1,
            Sort = "startDateAsc",
        };
        var specification = new AppointmentWithFiltersForCountSpecification(paginationSpecification);
        var dbContext = await GetDatabaseContext();
        var appointmentRepository = new GenericRepository<Appointment>(dbContext);

        //Act
        var result = appointmentRepository.CountAsync(specification);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<int>));
    }

    [Theory]
    [InlineData(1)]
    public async void AppointmentRepository_GetEntityWithSpec_ReturnsAppointment(int id)
    {
        //Arrange 
        var specification = new AppointmentsWithExaminationsSpecifications(id);
        var dbContext = await GetDatabaseContext();
        var appointmentRepository = new GenericRepository<Appointment>(dbContext);

        //Act
        var result = appointmentRepository.GetEntityWithSpec(specification);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<Appointment>));
    }

    [Fact]
    public async void AppointmentRepository_Add_ReturnsInt()
    {
        //Arrange
        var entity = new Appointment()
        {
            DoctorId = 2,
            ClientId = 3,
            Description = "Add Test",
            Title = "Add Test",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
        };
        var dbContext = await GetDatabaseContext();
        var appointmentRepository = new GenericRepository<Appointment>(dbContext);

        //Act
        appointmentRepository.Add(entity);
        var result =  dbContext.SaveChanges();

        //Assert
        result.Should().Be(1);
        result.Should().BeOfType(typeof(int));
    }


    [Fact]
    public async void AppointmentRepository_Update_ReturnsInt()
    {
        //Arrange
        var dbContext = await GetDatabaseContext();
        var appointmentRepository = new GenericRepository<Appointment>(dbContext);
        var entity = await appointmentRepository.GetByIdAsync(10);

        //Act
        appointmentRepository.Update(entity);
        var result = await dbContext.SaveChangesAsync();

        //Assert
        result.Should().Be(1);
        result.Should().BeOfType(typeof(int));
    }

    [Fact]
    public async void AppointmentRepository_Remove_ReturnsInt()
    {
        //Arrange
        var dbContext = await GetDatabaseContext();
        var appointmentRepository = new GenericRepository<Appointment>(dbContext);
        var entity = await appointmentRepository.GetByIdAsync(10);

        //Act
        appointmentRepository.Delete(entity);
        var result = await dbContext.SaveChangesAsync();

        //Assert
        result.Should().Be(1);
        result.Should().BeOfType(typeof(int));
    }
}