using System.ComponentModel.DataAnnotations;

namespace University.Core.Helper;

public class Helper
{
    public static bool EmailIsValid(string email)
    {
        var emailAddressAttribute = new EmailAddressAttribute();
        return emailAddressAttribute.IsValid(email);
    }
}