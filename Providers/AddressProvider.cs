using Detailing.Models;
using Detailing.Models.Google;

namespace Detailing.Providers;

public class AddressProvider
{
    private const string ApiBaseUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=";
    private const string ApiKeyQueryString = "&key=";
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient = new HttpClient();

    public AddressProvider(IConfiguration config)
    {
        _config = config;
    }
    
    public async Task<Location> GetCoordinatesAsync(Address address)
    {
        var apiKey = _config.GetSection("Google").GetSection("ApiKey").Value;
        var uri = new Uri($"{ApiBaseUrl}{address.ToString()}{ApiKeyQueryString}{apiKey}");
        var response = await _httpClient.GetFromJsonAsync<ResultsContainer>(uri);
        if (response.Status.ToLower() == "ok")
        {
            Console.WriteLine(response);
            return response.Results[0].Geometry.Location;
        }

        return new Location();
    }
}