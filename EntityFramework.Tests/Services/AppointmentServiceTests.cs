using AutoMapper;
using EntityFramework.BLL.Dtos;
using EntityFramework.BLL.Dtos.Requests;
using EntityFramework.BLL.Dtos.Responses;
using EntityFramework.BLL.Helpers;
using EntityFramework.BLL.Services;
using EntityFramework.BLL.Specifications;
using EntityFramework.DAL.Interfaces;
using EntityFramework.DAL.Models;
using FakeItEasy;
using FluentAssertions;

namespace EntityFramework.Tests.Services;

public class AppointmentServiceTests
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AppointmentService _appointmentService;

    public AppointmentServiceTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _mapper = A.Fake<IMapper>();
        _appointmentService = new AppointmentService(_unitOfWork, _mapper);
    }

    [Fact]
    public async Task AppointmentService_GetAppointments_ReturnsAppointmentsResponse()
    {
        //Arrange
        var paginationSpecification = new PaginationSpecificationParams()
        {
            PageIndex = 1,
            PageSize = 1,
            Sort = "startDateAsc",
        };
        var appointments = A.Fake<IReadOnlyList<Appointment>>();
        var specification = new AppointmentsWithExaminationsSpecifications(paginationSpecification);
        A.CallTo(()=>_unitOfWork.Repository<Appointment>().ListAsync(specification))
            .Returns(appointments);
        var countAppointmentsSpecification =
            new AppointmentWithFiltersForCountSpecification(paginationSpecification);
        A.CallTo( () => _unitOfWork.Repository<Appointment>().CountAsync(countAppointmentsSpecification))
            .Returns(1);

        //Act
        var result = _appointmentService.GetAppointmentsAsync(paginationSpecification);

        //Asserts
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<Pagination<AppointmentResponse>>));
    }

    [Theory]
    [InlineData(2)]
    public void AppointmentService_GetAppointmentById_ReturnsAppointmentResponse(int id)
    {
        //Arrange
        var specification = new AppointmentsWithExaminationsSpecifications(id);
        var appointment = A.Fake<Appointment>();
        appointment.Id = id;
        A.CallTo(() => _unitOfWork.Repository<Appointment>().GetEntityWithSpec(specification))
            .Returns(appointment);
        
        //Act
        var result = _appointmentService.GetAppointmentByIdAsync(id);
        
        //Asserts
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<AppointmentResponse>));
    }

    [Fact]
    public void AppointmentService_CreateAppointment_ReturnsCreateAppointment()
    {
        //Arrange
        var createAppointmentRequest = new CreateAppointmentRequest();
        A.CallTo(() =>
            _unitOfWork.Repository<Appointment>()
                .Add(_mapper.Map<CreateAppointmentRequest, Appointment>(
                    createAppointmentRequest)));
        A.CallTo(() => _unitOfWork.Complete()).Returns(1);

        //Act
        var result = _appointmentService.CreateAppointmentAsync(createAppointmentRequest);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<CreateAppointmentRequest>));
    }

    [Fact]
    public void AppointmentService_UpdateAppointment_ReturnsUpdateAppointmentRequest()
    {
        //Arrange
        var updateAppointmentRequest = new UpdateAppointmentRequest();
        A.CallTo(() =>
            _unitOfWork.Repository<Appointment>()
                .Update(_mapper.Map<UpdateAppointmentRequest, Appointment>(
                    updateAppointmentRequest)));
        A.CallTo(() => _unitOfWork.Complete()).Returns(1);

        //Act
        var result = _appointmentService.UpdateAppointmentAsync(updateAppointmentRequest);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<UpdateAppointmentRequest>));
    }
    
    [Fact]
    public void AppointmentService_DeleteAppointment_ReturnsDeleteAppointmentRequest()
    {
        //Arrange
        var deleteAppointmentRequest = new DeleteAppointmentRequest();
        A.CallTo(() =>
            _unitOfWork.Repository<Appointment>()
                .Delete(_mapper.Map<DeleteAppointmentRequest, Appointment>(
                    deleteAppointmentRequest)));
        A.CallTo(() => _unitOfWork.Complete()).Returns(1);

        //Act
        var result = _appointmentService.DeleteAppointmentAsync(deleteAppointmentRequest);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<DeleteAppointmentRequest>));
    }
}