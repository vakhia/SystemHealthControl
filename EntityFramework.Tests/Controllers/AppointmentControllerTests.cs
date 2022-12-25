using EntityFramework.API.Controllers;
using EntityFramework.BLL.Dtos;
using EntityFramework.BLL.Dtos.Requests;
using EntityFramework.BLL.Helpers;
using EntityFramework.BLL.Interfaces;
using EntityFramework.BLL.Specifications;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EntityFramework.Tests.Controllers;

public class AppointmentControllerTests
{
    private readonly IAppointmentService _appointmentService;
    private readonly ILogger<AppointmentController> _logger;
    private readonly AppointmentController _appointmentController;

    public AppointmentControllerTests()
    {
        _appointmentService = A.Fake<IAppointmentService>();
        _logger = A.Fake<ILogger<AppointmentController>>();
        _appointmentController = new AppointmentController(_appointmentService, _logger);
    }

    [Fact]
    public void AppointmentController_GetAppointments_ReturnsPaginationAppointmentResponse()
    {
        //Arrange
        var paginationSpecification = new PaginationSpecificationParams()
        {
            PageIndex = 1,
            PageSize = 1,
            Sort = "startDateAsc",
        };
        var appointments = A.Fake<Pagination<AppointmentResponse>>();
        A.CallTo(() => _appointmentService.GetAppointmentsAsync(paginationSpecification)).Returns(appointments);

        //Act
        var result = _appointmentController.GetAppointments(paginationSpecification);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<ActionResult<Pagination<AppointmentResponse>>>));
    }

    [Theory]
    [InlineData(1)]
    public void AppointmentController_GetAppointmentById_ReturnsAppointmentResponse(int id)
    {
        //Arrange
        var appointment = A.Fake<AppointmentResponse>();
        A.CallTo(() => _appointmentService.GetAppointmentByIdAsync(id)).Returns(appointment);

        //Act
        var result = _appointmentController.GetAppointmentById(id);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<ActionResult<AppointmentResponse>>));
    }

    [Fact]
    public void AppointmentController_CreateAppointment_ReturnCreateAppointmentRequest()
    {
        //Arrange
        var createAppointmentRequest = A.Fake<CreateAppointmentRequest>();
        A.CallTo(() => _appointmentService.CreateAppointmentAsync(createAppointmentRequest))
            .Returns(createAppointmentRequest);

        //Act
        var result = _appointmentController.CreateAppointment(createAppointmentRequest);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<ActionResult<CreateAppointmentRequest>>));
    }

    [Fact]
    public void AppointmentController_UpdateAppointment_ReturnUpdateAppointmentRequest()
    {
        //Arrange
        var updateAppointmentRequest = A.Fake<UpdateAppointmentRequest>();
        A.CallTo(() => _appointmentService.UpdateAppointmentAsync(updateAppointmentRequest))
            .Returns(updateAppointmentRequest);

        //Act
        var result = _appointmentController.UpdateAppointment(updateAppointmentRequest);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<ActionResult<UpdateAppointmentRequest>>));
    }

    [Fact]
    public void AppointmentController_DeleteAppointment_ReturnDeleteAppointmentRequest()
    {
        //Arrange
        var deleteAppointmentRequest = A.Fake<DeleteAppointmentRequest>();
        A.CallTo(() => _appointmentService.DeleteAppointmentAsync(deleteAppointmentRequest))
            .Returns(deleteAppointmentRequest);

        //Act
        var result = _appointmentController.DeleteAppointment(deleteAppointmentRequest);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<ActionResult<DeleteAppointmentRequest>>));
    }
}