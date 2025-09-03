namespace ClientManager.src.Validators;

public static class CpfValidator
{
    // Lógica de validação de CPF
    public static bool IsValid(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        var multiplicador1 = new int[9] {10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplicador2 = new int[10] {11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        // Verificar se o cpf possui 11 dígitos
        if (cpf.Length != 11) return false;

        var tempCpf = cpf.Substring(0, 9); // Pegar os 9 dígitos iniciais

        // Validação do primeiro dígito
        var soma = 0;
        for (int i = 0; i < 9; i++)
        {
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        }

        var resto = soma % 11;
        if (resto < 2) resto = 0;
        else resto = 11 - resto;

        tempCpf += resto;

        // Validação do segundo dígito
        soma = 0;
        for (int i = 0; i < 10; i++)
        {
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        }

        resto = soma % 11;
        if (resto < 2) resto = 0;
        else resto = 11 - resto;

        return cpf.EndsWith(resto.ToString());

    }
}
