using System.Xml.Linq;

namespace GreekLogics;

public partial class BusinessRegistry
{
    private static string? GetBDResultValue(XElement searchTarget, string name)
    {
        return searchTarget.Descendants(_ns3Namespace + name).FirstOrDefault()?.Value; 
    }

    private static Business? ParseBusinessDetailsResponse(string xmlResponse)
    {
        var root = XElement.Parse(xmlResponse);
        const string dateFormat = "yyyy-MM-dd";

        var resultNode = 
            root.Descendants(_envNamespace + "Body").First()
                    .Descendants(_srvcNamespace + "rgWsPublic2AfmMethodResponse").First()
                        .Descendants(_srvcNamespace + "result").First()
                            .Descendants(_ns3Namespace + "rg_ws_public2_result_rtType").First();

        var basicRecNode = resultNode.Descendants(_ns3Namespace + "basic_rec").First();
        var firmActNode = resultNode.Descendants(_ns3Namespace + "firm_act_tab").First();

        var activity = new List<(string Description, BusinessActivityType aType)>();

        foreach(var item in firmActNode.Descendants(_ns3Namespace + "item"))
        {
            activity.Add(
                    (GetBDResultValue(item, "firm_act_descr") ?? "",
                     (BusinessActivityType) (int.Parse(GetBDResultValue(item, "firm_act_kind") ?? ""))));
        }

        var street = GetBDResultValue(basicRecNode, "postal_address");
        var streetNo = GetBDResultValue(basicRecNode, "postal_address_no");

        string? streetAddress = (street ?? "") + " " + (streetNo ?? "");
        if(streetAddress == " ")
        {
            streetAddress = null;
        }
        
        var registrationDate = GetBDResultValue(basicRecNode, "regist_date");
        var stopDateValue = GetBDResultValue(basicRecNode, "stop_date");
        
        #pragma warning disable CS8073 
        var result = new Business
        (
            taxId: GetBDResultValue(basicRecNode, "afm"),
            taxAuthority: GetBDResultValue(basicRecNode, "doy_descr"),
            isActive: (GetBDResultValue(basicRecNode, "deactivation_flag") ?? "") == "1",
            name: GetBDResultValue(basicRecNode, "onomasia"),
            discreetTitle: GetBDResultValue(basicRecNode, "commer_title"),
            streetAddress:streetAddress,
            postalCode: GetBDResultValue(basicRecNode, "postal_zip_code"),
            area: GetBDResultValue(basicRecNode, "postal_area_description"),
            registrationDate:
                string.IsNullOrWhiteSpace(registrationDate) ?
                DateOnly.FromDateTime(DateTime.Today) :
                DateOnly.ParseExact(registrationDate, dateFormat),
            stopDate: string.IsNullOrWhiteSpace(stopDateValue) ? null : DateOnly.ParseExact(stopDateValue, dateFormat),
            activity: activity
        );
        #pragma warning restore CS8073 

        return result;
    }
}
