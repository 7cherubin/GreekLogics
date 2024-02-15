using System.Xml.Linq;

namespace GreekLogics;

public partial class BusinessRegistry
{
    private static readonly XNamespace _soapNamespace = "http://www.w3.org/2003/05/soap-envelope";
    private static readonly XNamespace _rgwNamespace = "http://rgwspublic2/RgWsPublic2Service";

    private XElement GenerateAPIInfoRequestBody()
    {
        return new XElement(
        _soapNamespace  + "Envelope",
        new XAttribute(XNamespace.Xmlns + "soap", _soapNamespace),
        new XAttribute(XNamespace.Xmlns + "rgw", _rgwNamespace),
            new XElement(_soapNamespace + "Header"),
            new XElement(_soapNamespace + "Body",
                new XElement(_rgwNamespace + "rgWsPublic2VersionInfo")
            )
        );
    }
}
