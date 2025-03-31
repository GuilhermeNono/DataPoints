using System.Text.RegularExpressions;
using DataPoints.Domain.Enums;
using DataPoints.Domain.Enums.Entities;
using Premios.Domain.Helpers;

namespace DataPoints.Domain.Helpers;

public class DocumentHelper
{
    public readonly DocumentType PersonDocument;
    private readonly string _documentNumber;
    private static readonly IList<int> FirstNumberCpnjMultiplier = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
    private static readonly IList<int> SecondNumberCpnjMultiplier = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
    public string Normalized => ValidHelper.RemoveSpecialCharacters(_documentNumber);

    private DocumentHelper(DocumentType personDocument, string documentNumber)
    {
        PersonDocument = personDocument;
        _documentNumber = documentNumber;
    }

    public bool IsFormatedDocumentNumber =>
        Regex.IsMatch(_documentNumber, @"\d{3}\.\d{3}\.\d{3}-\d{2}") ||
        Regex.IsMatch(_documentNumber, @"\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}");

    public PersonType PersonType => PersonDocument == DocumentType.Cpf
        ? PersonType.NaturalPerson
        : PersonType.LegalPerson;

    public static DocumentHelper ConfigureHelper(string documentNumber)
    {
        var document = documentNumber.Length == 14 ? DocumentType.Cpf : DocumentType.Cnpj;
        return new(document, documentNumber);
    }

    public bool IsCpfValid()
    {
        if (string.IsNullOrEmpty(_documentNumber))
            return false;

        var documentNumberNormalized = ValidHelper.RemoveSpecialCharacters(_documentNumber);

        if (documentNumberNormalized.Length != 11)
            return false;

        if (documentNumberNormalized.All(c => c == documentNumberNormalized[0]))
            return false;

        var firstDigit = 0;
        for (var i = 0; i < 9; i++)
            firstDigit += int.Parse(documentNumberNormalized[i].ToString()) * (i + 1);

        var processedFirstDigitValue = (firstDigit % 11);
        firstDigit = (firstDigit % 11) >= 10 ? 0 : processedFirstDigitValue;

        var secondDigit = 0;
        for (var i = 0; i < 10; i++)
            secondDigit += int.Parse(documentNumberNormalized[i].ToString()) * i;

        var processedSecondDigitValue = (secondDigit % 11);
        secondDigit = (secondDigit % 11) >= 10 ? 0 : processedSecondDigitValue;

        return firstDigit == int.Parse(documentNumberNormalized[9].ToString()) &&
               secondDigit == int.Parse(documentNumberNormalized[10].ToString());
    }

    public bool IsCnpjValid()
    {
        if (string.IsNullOrEmpty(_documentNumber))
            return false;

        var documentNumberNormalized = ValidHelper.RemoveSpecialCharacters(_documentNumber);

        if (documentNumberNormalized.Length != 14)
            return false;

        if (documentNumberNormalized.All(c => c == documentNumberNormalized[0]))
            return false;

        var firstDigit = 0;

        for (var i = 0; i < 12; i++)
            firstDigit += int.Parse(documentNumberNormalized[i].ToString()) * FirstNumberCpnjMultiplier[i];

        firstDigit = (firstDigit % 11);

        if (firstDigit < 2)
            firstDigit = 0;
        else
            firstDigit = 11 - firstDigit;

        var secondDigit = 0;

        for (var i = 0; i < 13; i++)
            secondDigit += int.Parse(documentNumberNormalized[i].ToString()) * SecondNumberCpnjMultiplier[i];

        secondDigit = (secondDigit % 11);

        if (secondDigit < 2)
            secondDigit = 0;
        else
            secondDigit = 11 - secondDigit;

        return firstDigit == int.Parse(documentNumberNormalized[12].ToString()) &&
               secondDigit == int.Parse(documentNumberNormalized[13].ToString());
    }

    public static object MaskCpf(string trim)
    {
        throw new NotImplementedException();
    }
}
