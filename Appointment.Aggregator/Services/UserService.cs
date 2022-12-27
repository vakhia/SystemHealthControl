using Appointment.Aggregator.Extensions;
using Appointment.Aggregator.Interfaces;
using Appointment.Aggregator.Models;
using Polly;
using Polly.CircuitBreaker;

namespace Appointment.Aggregator.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    
    private static readonly AsyncCircuitBreakerPolicy<HttpResponseMessage> CircuitBreakerPolicy =
        Policy.HandleResult<HttpResponseMessage>(message => (int)message.StatusCode == 500)
            .CircuitBreakerAsync(2, TimeSpan.FromMinutes(3));

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserModel> GetUser(string userId)
    {
        if (CircuitBreakerPolicy.CircuitState == CircuitState.Open)
        {
            throw new Exception("Service is currently unavailable");
        }

        var response = await CircuitBreakerPolicy.ExecuteAsync(() =>
            _httpClient.GetAsync($"/api/Account/{userId}"));

        return await response.ReadContentAs<UserModel>();
        
    }
}