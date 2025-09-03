using System.ComponentModel.DataAnnotations;
using ClientManager.src.Exceptions;

namespace ClientManager.src.DTOs.Base;

public class BaseDto
{
    public void Validate()
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(this);

        if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
        {
            var errors = validationResults
                .Where(vr => vr.ErrorMessage != null)
                .Select(vr => vr.ErrorMessage);

            throw new RequestValidationException(string.Join(" | ", errors));
        }
    }
}
