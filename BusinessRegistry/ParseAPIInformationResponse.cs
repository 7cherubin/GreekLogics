using System.Xml.Linq;

namespace GreekLogics;

public partial class BusinessRegistry
{
    private static readonly XNamespace _srvcNamespace = "http://rgwspublic2/RgWsPublic2Service";

    private static string ParseAPIInformationResponse(string xmlResponse)
    {
        var root = XElement.Parse(xmlResponse);

        return root.Descendants(_envNamespace + "Body")
            .Descendants(_srvcNamespace + "rgWsPublic2VersionInfoResponse")
                .Descendants(_srvcNamespace + "result").First().Value;
    }
}
