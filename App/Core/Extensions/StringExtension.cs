namespace Core.Extensions;

public static class StringExtensions
{
    public static string ToBase64(this string text) => Convert.ToBase64String(Encoding.UTF8.GetBytes(text));

    public static string FromBase64(this string text) => Encoding.UTF8.GetString(Convert.FromBase64String(text));

    public static bool IsYesOrNo(this string text) => string.IsNullOrEmpty(text) || text == "S" || text == "N";

    public static string ExtractNumbers(this string value) => Regex.Replace(value, @"\D", string.Empty);

    public static float TryToFloat(this string value) => float.TryParse(value, out float numero) ? numero : default;

    public static long TryToLong(this string value) => long.TryParse(value, out long numero) ? numero : default;

    public static int TryToInt(this string value) => int.TryParse(value, out int numero) ? numero : default;

    public static DateTime TryToDateTime(this string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length < 8)
            return default;

        int.TryParse(value.Substring(0, 4), out int year);
        int.TryParse(value.Substring(4, 2), out int month);
        int.TryParse(value.Substring(6, 2), out int day);

        int.TryParse(value.Length >= 10 ? value.Substring(8, 2) : "0", out int hour);
        int.TryParse(value.Length >= 12 ? value.Substring(10, 2) : "0", out int minute);
        int.TryParse(value.Length >= 14 ? value.Substring(12, 2) : "0", out int second);

        year = Math.Max(year, 1);
        month = Math.Clamp(month, 1, 12);
        day = Math.Clamp(day, 1, DateTime.DaysInMonth(year, month));

        hour = Math.Clamp(hour, 0, 23);
        minute = Math.Clamp(minute, 0, 59);
        second = Math.Clamp(second, 0, 59);

        return new DateTime(year, month, day, hour, minute, second);
    }
    
    public static string RemoveDiacritics(this string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

        for (int i = 0; i < normalizedString.Length; i++)
        {
            char c = normalizedString[i];
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                stringBuilder.Append(c);
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    public static bool IsValidCnpj(this string value)
    {
        const string sequencia = "6543298765432";

        var cnpj = value.ExtractNumbers().PadLeft(14, '0');
        if (cnpj.Distinct().Count() == 1 || cnpj.Length != 14)
            return false;

        var d = new int[14];
        var v = new int[2];
        int i;

        for (i = 0; i <= 13; i++)
            d[i] = Convert.ToInt32(cnpj.Substring(i, 1));

        for (i = 0; i <= 1; i++)
        {
            var soma = 0;
            int j;
            for (j = 0; j <= 11 + i; j++)
                soma += d[j] * Convert.ToInt32(sequencia.Substring(j + 1 - i, 1));

            v[i] = (soma * 10) % 11;
            if (v[i] == 10) v[i] = 0;
        }

        return v[0] == d[12] && v[1] == d[13];
    }
}
