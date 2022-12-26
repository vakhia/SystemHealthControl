using Grpc.Core;
using GrpcService.Protos;

namespace GrpcService.Services;

public class ValidatorService : DataValidator.DataValidatorBase 
{
    public override Task<UserDataResponse> ValidatePersonalData(UserDataRequest request, ServerCallContext context)
    {
        return Task.FromResult(new UserDataResponse()
        {
            PassportNumber = true,
            CreditCard = true,
        });
    }
}