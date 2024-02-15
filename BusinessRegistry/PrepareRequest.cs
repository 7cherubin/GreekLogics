using System.Xml.Linq;
using System.Text;

namespace GreekLogics;

public partial class BusinessRegistry
{
    private static readonly Uri _businessRegistryServiceUri = new Uri("https://www1.gsis.gr/wsaade/RgWsPublic2/RgWsPublic2");
    private const string _soapActionBusinessDetails = "http://rgwspublic2/RgWsPublic2Service:rgWsPublic2AfmMethod";
    private const string _soapActionAPIInfo = "http://rgwspublic2/RgWsPublic2Service:rgWsPublic2VersionInfo";

    private static HttpRequestMessage PrepareRequest(XElement content, string soapAction)
    {
        var request = new HttpRequestMessage()
        {
            Content = new StringContent(content.ToString(), Encoding.UTF8, "application/soap+xml"),
            Method = HttpMethod.Post,
            RequestUri = _businessRegistryServiceUri,
        };

        request.Headers.Add("SOAPAction", soapAction);

        return request;
    }
}
