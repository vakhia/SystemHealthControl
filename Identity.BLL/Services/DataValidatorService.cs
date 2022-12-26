using GrpcService.Protos;
using GrpcService.Services;
using Identity.BLL.Interfaces;

namespace Identity.BLL.Services;

public class DataValidatorService : IDataValidatorService
{
    private readonly DataValidator.DataValidatorClient _dataValidatorClient;

    public DataValidatorService(DataValidator.DataValidatorClient dataValidatorClient)
    {
        _dataValidatorClient = dataValidatorClient;
    }

    public async Task<UserDataResponse> ValidateData(UserDataRequest userDataRequest)
    {
        return await _dataValidatorClient.ValidatePersonalDataAsync(userDataRequest);
    }
}