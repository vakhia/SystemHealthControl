using Identity.BLL.Interfaces;
using RestSharp;
using RestSharp.Authenticators;

namespace Identity.BLL.Services;

public class MailService: IMailService
{
    public bool SendEmail(string body, string email)
    {
        var client = new RestClient("https://api.mailgun.net/v3");
        var request = new RestRequest("", Method.Post);
        client.Authenticator = new HttpBasicAuthenticator("api", "9f00eae27ee75989653b24a374bc5679-eb38c18d-900ea9e6");
        request.AddParameter("domain", "sandboxee85464862b94cf9963ce5f39d5aa3c4.mailgun.org", ParameterType.UrlSegment);
        request.AddParameter("from", "Excited User <mailgun@YOUR_DOMAIN_NAME");
        request.AddParameter("to", email);
        request.AddParameter("subject", "Email Verification");
        request.AddParameter("text", body);
        request.Method = Method.Post;

        var response = client.Execute(request);

        return response.IsSuccessful;
    }
}