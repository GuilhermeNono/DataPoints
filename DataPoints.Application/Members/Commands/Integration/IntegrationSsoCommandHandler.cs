using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DataPoints.Application.Members.Abstractions.Commands;
using DataPoints.Contract.Integration.Response;

namespace DataPoints.Application.Members.Commands.Integration;

public class IntegrationSsoCommandHandler : ICommandHandler<IntegrationSsoCommand, IntegrationSsoResponse>
{
    
    private readonly HttpClient _httpClient = new ();
    
    public async Task<IntegrationSsoResponse> Handle(IntegrationSsoCommand request, CancellationToken cancellationToken)
    {

        var body = JsonSerializer.Serialize(request);
        
        var message = new HttpRequestMessage()
        {
            RequestUri = new Uri("https://localhost:7178/v1/integrations/token"),
            Method = HttpMethod.Post,
            Content = new StringContent(body)
        };
        
        var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        var clientId = "9d2bcbd0-c069-40e7-8674-57b97e2c441c";
        
        message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        
        message.Headers.Add("X-TimeStamp", timeStamp);
        message.Headers.Add("X-ClientId", clientId);

        var signature =
            $"{timeStamp}{clientId}{message.Method}{message.RequestUri!.AbsolutePath}{message.RequestUri.Query}{body}";

        var hash = GenerateSignature("fa4fb663-ed8d-482c-aaf4-eb6c2e80ceb5", signature);

        message.Headers.Add("X-Signature", hash);

        var response = await _httpClient.SendAsync(message, cancellationToken);
        
        var serializedResponse = await response.Content.ReadAsStringAsync(cancellationToken);

        IntegrationSsoResponse? deserializedResponse = null;
        
        if (!response.IsSuccessStatusCode)
            throw new Exception();
        
        try
        {
            deserializedResponse = JsonSerializer.Deserialize<IntegrationSsoResponse>(serializedResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            if(deserializedResponse == null)
                throw new Exception();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return deserializedResponse;
    }
    
    private static string GenerateSignature(string secret, string signature)
    {
        var secretBytes = Encoding.UTF8.GetBytes(secret);
        var key = new HMACSHA256(secretBytes);
        var hashBytes = key.ComputeHash(Encoding.UTF8.GetBytes(signature));
        return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
    }
}