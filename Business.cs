namespace GreekLogics;

public record struct Business
{
    public string TaxId { get; }
    public bool IsActive { get; }
    public string TaxAuthority { get; }
    public string Name { get; }
    public string? DiscreetTitle { get; }
    public string? StreetAddress { get; }
    public string PostalCode { get; }
    public string Area { get; }
    public DateOnly RegistrationDate { get; }
    public DateOnly? StopDate { get; }
    public List<(string Description, BusinessActivityType AType)> Activity { get; }

    public Business(
        string? taxId,
        string? taxAuthority,
        string? name,
        string? area,
        string? postalCode,
        List<(string description, BusinessActivityType aType)>? activity,
        DateOnly registrationDate,
        bool isActive,
        DateOnly? stopDate = null,
        string? discreetTitle = null,
        string? streetAddress = null
    )
    {
       if(string.IsNullOrWhiteSpace(taxId))
       {
           throw new ArgumentNullException(nameof(taxId));
       }

       if(string.IsNullOrWhiteSpace(taxAuthority))
       {
           throw new ArgumentNullException(nameof(taxAuthority));
       }

       if(string.IsNullOrWhiteSpace(name))
       {
           throw new ArgumentNullException(nameof(name));
       }

       if(string.IsNullOrWhiteSpace(area))
       {
           throw new ArgumentNullException(nameof(area));
       }

       if(string.IsNullOrWhiteSpace(postalCode))
       {
           throw new ArgumentNullException(nameof(postalCode));
       }

       if(activity == null || activity.Count == 0)
       {
           throw new ArgumentNullException(nameof(activity));
       }

        if(isActive == false && stopDate == null)
        {
            throw new ArgumentNullException(nameof(stopDate));
        }

        TaxId = taxId;
        TaxAuthority = taxAuthority;
        Name = name;
        Area = area;
        PostalCode = postalCode;
        Activity = activity;
        RegistrationDate = registrationDate;
        IsActive = isActive;
        StopDate = stopDate;
        DiscreetTitle = discreetTitle;
        StreetAddress = streetAddress;
    }
}
