namespace GreekLogics;

public partial class BusinessRegistry
{
    private readonly string _username;
    private readonly string _password;
    private static readonly HttpClient _client = new HttpClient();

    public BusinessRegistry(string username, string password)
    {
        if(username == null)
        {
            throw new ArgumentNullException(nameof(username));
        }
        else if(username.Length == 0)
        {
            throw new Exception($"{nameof(username)} is empty.");
        }

        if(password == null)
        {
            throw new ArgumentNullException(nameof(password));
        }
        else if(password.Length == 0)
        {
            throw new Exception($"{nameof(password)} is empty.");
        }

        _username = username;
        _password = password;
    }

    public async Task<Business?> RequestBusinessDetails(string taxId, DateOnly? asOnDate = null)
    {
        if(Validate.TaxId(taxId) == false)
        {
            throw new InvalidTaxIdException();
        }
        
        var request = PrepareRequest(GenerateBusinessDetailsRequestBody(taxId, asOnDate),
                _soapActionBusinessDetails);

        var response = await _client.SendAsync(request);
        if(response.IsSuccessStatusCode == false)
        {
            throw new Exception($"Response: {response.StatusCode} - {response.ReasonPhrase}");
        }
        try
        {
            return ParseBusinessDetailsResponse(await response.Content.ReadAsStringAsync());
        }
        catch(Exception)
        {
            return null;
        }
    }

    public async Task<string> GetAPIInformation()
    {
        var request = PrepareRequest(GenerateAPIInfoRequestBody(), _soapActionAPIInfo);
        
        var response = await _client.SendAsync(request);

        if(response.IsSuccessStatusCode == false)
        {
            throw new Exception($"Response: {response.StatusCode} - {response.ReasonPhrase}");
        }

        return ParseAPIInformationResponse(await response.Content.ReadAsStringAsync());
    }
}
