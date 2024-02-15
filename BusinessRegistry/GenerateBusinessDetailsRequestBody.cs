using System.Xml.Linq;

namespace GreekLogics;

public partial class BusinessRegistry
{
    private static readonly XNamespace _envNamespace = "http://www.w3.org/2003/05/soap-envelope";
    private static readonly XNamespace _ns1Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";
    private static readonly XNamespace _ns2Namespace = "http://rgwspublic2/RgWsPublic2Service";
    private static readonly XNamespace _ns3Namespace = "http://rgwspublic2/RgWsPublic2";

    private XElement GenerateBusinessDetailsRequestBody(string taxId, DateOnly? asOnDate)
    {
        var result = new XElement(
            _envNamespace + "Envelope",
            new XAttribute(XNamespace.Xmlns + "env", _envNamespace),
            new XAttribute(XNamespace.Xmlns + "ns1", _ns1Namespace),
            new XAttribute(XNamespace.Xmlns + "ns2", _ns2Namespace),
            new XAttribute(XNamespace.Xmlns + "ns3", _ns3Namespace),
                new XElement(_envNamespace + "Header",
                    new XElement(_ns1Namespace + "Security",
                        new XElement(_ns1Namespace + "UsernameToken",
                            new XElement(_ns1Namespace + "Username", _username),
                            new XElement(_ns1Namespace + "Password", _password)
                        )
                    )
                ),
                new XElement(_envNamespace + "Body",
                    new XElement(_ns2Namespace + "rgWsPublic2AfmMethod",
                        new XElement(_ns2Namespace + "INPUT_REC",
                            new XElement(_ns3Namespace + "afm_called_for", taxId)
                        )
                    )
                )
            );

        if(asOnDate != null)
        {
            result
                .Descendants(_envNamespace + "Body")
                    .Descendants(_ns2Namespace + "rgWsPublic2AfmMethod")
                        .Descendants(_ns2Namespace + "INPUT_REC").First().Add(new XElement(_ns3Namespace + "as_on_date", asOnDate.Value.ToString("yyyy-MM-dd")));
        }

        return result;
    }
}
