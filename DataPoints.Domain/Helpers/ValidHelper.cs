using System.Globalization;
using System.Text.RegularExpressions;
using DataPoints.Domain.Helpers;

namespace Premios.Domain.Helpers;

public static class ValidHelper
{
    private const string MASK_DATA = @"dd\/MM\/yyyy";
    public static DocumentHelper DocumentValidate(string documentNumber)
    {
        if (string.IsNullOrWhiteSpace(documentNumber))
            throw new Exception();

        return DocumentHelper.ConfigureHelper(documentNumber);
    }

    public static string LikeContem(string value)
    {
        return string.Concat("%", value, "%");
    }
    
    public static string DataParaTexto(DateTime? data)
    {
        if (!data.HasValue)
            return "";

        return data.Value.ToString(MASK_DATA, new CultureInfo("pt-BR"));
    }
    public static string RemoveSpecialCharacters(string stringToNormalize) =>
        Regex.Replace(stringToNormalize, @"\D", string.Empty);

    public static string ValueCurrency(decimal value)
    {
        return value.ToString("C2");
    }
}
