using ClientManager.src.Validators;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ClientManager.src.Attributes;

public class CpfAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null) return true;

        var cpf = value.ToString();

        if (string.IsNullOrWhiteSpace(cpf)) return false;

        cpf = Regex.Replace(cpf, @"[^\d]", "");

        if (cpf.Length < 11 || cpf.Length > 14) return false;

        return CpfValidator.IsValid(cpf);
    }
}
