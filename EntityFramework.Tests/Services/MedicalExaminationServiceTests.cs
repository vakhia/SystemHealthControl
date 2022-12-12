using AutoMapper;
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

public class MedicalExaminationServiceTests
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly MedicalExaminationService _medicalExaminationService;

    public MedicalExaminationServiceTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _mapper = A.Fake<IMapper>();
        _medicalExaminationService = new MedicalExaminationService(_unitOfWork, _mapper);
    }

    [Fact]
    public async Task MedicalExaminationService_GetMedicalExaminations_ReturnsMedicalExaminationsResponse()
    {
        //Arrange
        var paginationSpecification = new PaginationSpecificationParams()
        {
            PageIndex = 1,
            PageSize = 1,
            Sort = "titleAsc",
        };
        var medicalExaminations = A.Fake<IReadOnlyList<MedicalExamination>>();
        var specification = new MedicalExaminationsWithAppointmentSpecification(paginationSpecification);
        A.CallTo(() => _unitOfWork.Repository<MedicalExamination>().ListAsync(specification))
            .Returns(medicalExaminations);
        var countMedicalExaminationsSpecification =
            new MedicalExaminationsWithFiltersForCountSpecification(paginationSpecification);
        A.CallTo( () => _unitOfWork.Repository<MedicalExamination>().CountAsync(countMedicalExaminationsSpecification))
            .Returns(1);

        //Act
        var result = _medicalExaminationService.GetMedicalExaminationsAsync(paginationSpecification);

        //Asserts
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<Pagination<MedicalExaminationResponse>>));
    }

    [Theory]
    [InlineData(1)]
    public void MedicalExaminationService_GetMedicalExaminationById_ReturnsMedicalExaminationById(int id)
    {
        //Arrange
        var specification = new MedicalExaminationsWithAppointmentSpecification(id);
        var medicalExamination = A.Fake<MedicalExamination>();
        medicalExamination.Id = 1;
        A.CallTo(() => _unitOfWork.Repository<MedicalExamination>().GetEntityWithSpec(specification))
            .Returns(medicalExamination);
        
        //Act
        var result = _medicalExaminationService.GetMedicalExaminationByIdAsync(id);
        
        
        //Asserts
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<MedicalExaminationResponse>));
    }

    [Fact]
    public void MedicalExaminationService_CreateMedicalExamination_ReturnsCreateMedicalExamination()
    {
        //Arrange
        var createMedicalExaminationRequest = new CreateMedicalExaminationRequest();
        A.CallTo(() =>
            _unitOfWork.Repository<MedicalExamination>()
                .Add(_mapper.Map<CreateMedicalExaminationRequest, MedicalExamination>(
                    createMedicalExaminationRequest)));
        A.CallTo(() => _unitOfWork.Complete()).Returns(1);

        //Act
        var result = _medicalExaminationService.CreateMedicalExaminationAsync(createMedicalExaminationRequest);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<CreateMedicalExaminationRequest>));
    }

    [Fact]
    public void MedicalExaminationService_UpdateMedicalExamination_ReturnsUpdateMedicalExaminationRequest()
    {
        //Arrange
        var updateMedicalExaminationRequest = new UpdateMedicalExaminationRequest();
        A.CallTo(() =>
            _unitOfWork.Repository<MedicalExamination>()
                .Update(_mapper.Map<UpdateMedicalExaminationRequest, MedicalExamination>(
                    updateMedicalExaminationRequest)));
        A.CallTo(() => _unitOfWork.Complete()).Returns(1);

        //Act
        var result = _medicalExaminationService.UpdateMedicalExaminationAsync(updateMedicalExaminationRequest);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<UpdateMedicalExaminationRequest>));
    }
    
    [Fact]
    public void MedicalExaminationService_DeleteMedicalExamination_ReturnsDeleteMedicalExaminationRequest()
    {
        //Arrange
        var deleteMedicalExaminationRequest = new DeleteMedicalExaminationRequest();
        A.CallTo(() =>
            _unitOfWork.Repository<MedicalExamination>()
                .Delete(_mapper.Map<DeleteMedicalExaminationRequest, MedicalExamination>(
                    deleteMedicalExaminationRequest)));
        A.CallTo(() => _unitOfWork.Complete()).Returns(1);

        //Act
        var result = _medicalExaminationService.DeleteMedicalExaminationAsync(deleteMedicalExaminationRequest);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(Task<DeleteMedicalExaminationRequest>));
    }
}