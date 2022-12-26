using System.Text.RegularExpressions;
using CreditCardValidator;
using Grpc.Core;
using GrpcService.Protos;

namespace GrpcService.Services;

public class ValidatorService : DataValidator.DataValidatorBase
{
    public override Task<UserDataResponse> ValidatePersonalData(UserDataRequest request, ServerCallContext context)
    {
        return Task.FromResult(new UserDataResponse()
        {
            PassportNumber = ValidatePassportNumber(request.PassportNumber),
            CreditCard = ValidateCreditCard(request.CreditCard),
        });
    }

    private static bool ValidateCreditCard(string creditCard)
    {
        var creditCardDetector = new CreditCardDetector(creditCard);
        if (!creditCardDetector.IsValid())
        {
            return false;
        }

        switch (creditCardDetector.Brand)
        {
            case CardIssuer.Visa:
                return true;
            case CardIssuer.MasterCard:
                return true;
            default:
                return false;
        }
    }

    private static bool ValidatePassportNumber(string passportNumber)
    {
        const string regex = @"^[A-PR-WYa-pr-wy][1-9]\d\s?\d{4}[1-9]$";
        var result = new Regex(regex);
        return result.IsMatch(passportNumber);
    }
}