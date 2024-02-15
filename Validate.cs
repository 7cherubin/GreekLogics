using System.Text.RegularExpressions;

namespace GreekLogics;

/**
 * <summary>
 * Validate common data in Greece such as an entity's tax id, postal code etc.
 * </summary>
 */
public static class Validate 
{
    /**
     * <summary>
     * Validate a tax id.
     * </summary>
     * <remark>
     * The fact that a tax id is valid does not mean it exists.
     * </remarks>
     */
    public static bool TaxId(string? taxId)
    {
        const int TAX_ID_LENGTH = 9;
        var taxIdRegex = new Regex($@"\d{{{TAX_ID_LENGTH}}}");

        if(taxId == null || taxIdRegex.IsMatch(taxId) == false)
        {
            return false;
        }

        var digits = new List<int>();
        foreach(var ch in taxId)
        {
            try
            {
                digits.Add(int.Parse(ch.ToString()));
            }
            catch(Exception)
            {
                return false;
            }
        }

        int sum = 0;
        for(int i = 0; i < TAX_ID_LENGTH - 1; ++i)
        {
            sum += digits[i] * (int) Math.Pow(2, TAX_ID_LENGTH - i - 1);
        }

        var mod = sum % 11;
        if(mod == 10 && digits.Last() == 0)
        {
            return true;
        }
        else if(mod == digits.Last())
        {
            return true;
        }

        return false;
    }
}
