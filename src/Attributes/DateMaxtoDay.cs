using System.ComponentModel.DataAnnotations;

namespace ClientManager.src.Attributes;

public class DateMaxtoDayAttribute : ValidationAttribute
{
    private readonly DateOnly _minDate;

    public DateMaxtoDayAttribute(string minDate = "1900-01-01")
    {
        _minDate = DateOnly.Parse(minDate);
    }

    public override bool IsValid(object? value)
    {
        // Se for null → deixa para o [Required] tratar
        if (value == null) 
            return true;

        // Se for DateOnly
        if (value is DateOnly date)
        {
            return date >= _minDate && date <= DateOnly.FromDateTime(DateTime.Today);
        }

        // Se for DateTime
        if (value is DateTime dt)
        {
            var data = DateOnly.FromDateTime(dt);
            return data >= _minDate && data <= DateOnly.FromDateTime(DateTime.Today);
        }

        // Se for string → tenta converter
        if (value is string str && DateOnly.TryParse(str, out var parsed))
        {
            return parsed >= _minDate && parsed <= DateOnly.FromDateTime(DateTime.Today);
        }

        // Qualquer outro tipo é inválido
        return false;
    }
}
