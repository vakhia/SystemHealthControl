using EntityFramework.API.Controllers;
using EntityFramework.BLL.Dtos;
using EntityFramework.BLL.Dtos.Requests;
using EntityFramework.BLL.Dtos.Responses;
using EntityFramework.BLL.Helpers;
using EntityFramework.BLL.Interfaces;
using EntityFramework.BLL.Specifications;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework.Tests.Controllers;

public class MedicalExaminationControllerTests
{
    private readonly IMedicalExaminationService _medicalExaminationService;
    private readonly MedicalExaminationController _medicalExaminationController;

    public MedicalExaminationControllerTests()
    {
        _medicalExaminationService = A.Fake<IMedicalExaminationService>();
        _medicalExaminationController = new MedicalExaminationController(_medicalExaminationService);
    }
    
    [Fact]
    public void MedicalExaminationController_GetAppointments_ReturnsPaginationAppointmentResponse()
    {
        //Arrange
        var paginationSpecification = new PaginationSpecificationParams()
        {
            PageIndex = 1,
            PageSize = 1,
            Sort = "startDateAsc",
        };
        var medicalExaminations = A.Fake<Pagination<MedicalExaminationResponse>>();
        A.CallTo(() => _medicalExaminationService.GetMedicalExaminationsAsync(paginationSpecification))
            .Returns(medicalExaminations);

        //Act
        var result = _medicalExaminationController.GetMedicalExaminations(paginationSpecification);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<ActionResult<Pagination<MedicalExaminationResponse>>>));
    }

    [Theory]
    [InlineData(1)]
    public void MedicalExaminationController_GetMedicalExaminationById_ReturnsMedicalExaminationResponse(int id)
    {
        //Arrange
        var  medicalExamination = A.Fake<MedicalExaminationResponse>();
        A.CallTo(() => _medicalExaminationService.GetMedicalExaminationByIdAsync(id)).Returns(medicalExamination);

        //Act
        var result = _medicalExaminationController.GetMedicalExaminationById(id);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<ActionResult<MedicalExaminationResponse>>));
    }

    [Fact]
    public void MedicalExaminationController_CreateMedicalExamination_ReturnCreateMedicalExaminationRequest()
    {
        //Arrange
        var createMedicalExaminationRequest = A.Fake<CreateMedicalExaminationRequest>();
        A.CallTo(() => _medicalExaminationService.CreateMedicalExaminationAsync(createMedicalExaminationRequest))
            .Returns(createMedicalExaminationRequest);
        
        //Act
        var result = _medicalExaminationController.CreateMedicalExamination(createMedicalExaminationRequest);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<ActionResult<CreateMedicalExaminationRequest>>));
    }

    [Fact]
    public void MedicalExaminationController_UpdateMedicalExamination_ReturnUpdateMedicalExaminationRequest()
    {
        //Arrange
        var updateMedicalExaminationRequest = A.Fake<UpdateMedicalExaminationRequest>();
        A.CallTo(() => _medicalExaminationService.UpdateMedicalExaminationAsync(updateMedicalExaminationRequest))
            .Returns(updateMedicalExaminationRequest);
        
        //Act
        var result = _medicalExaminationController.UpdateMedicalExamination(updateMedicalExaminationRequest);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<ActionResult<UpdateMedicalExaminationRequest>>));
    }

    [Fact]
    public void MedicalExaminationController_DeleteMedicalExamination_ReturnDeleteMedicalExaminationRequest()
    {
        //Arrange
        var deleteMedicalExaminationRequest = A.Fake<DeleteMedicalExaminationRequest>();
        A.CallTo(() => _medicalExaminationService.DeleteMedicalExaminationAsync(deleteMedicalExaminationRequest))
            .Returns(deleteMedicalExaminationRequest);
        
        //Act
        var result = _medicalExaminationController.DeleteMedicalExamination(deleteMedicalExaminationRequest);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<ActionResult<DeleteMedicalExaminationRequest>>));
    }
}