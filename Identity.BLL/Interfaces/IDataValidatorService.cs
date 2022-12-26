using GrpcService.Protos;

namespace Identity.BLL.Interfaces;

public interface IDataValidatorService
{ 
    Task<UserDataResponse> ValidateData(UserDataRequest userDataRequest);
}