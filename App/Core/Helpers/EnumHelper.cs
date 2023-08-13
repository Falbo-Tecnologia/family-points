namespace Core.Helpers;

public class EnumHelper
{
    public static IEnumerable<T> CastValues<TEnum, T>(Func<TEnum, T> converter) where T : class
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<object>()
            .Select(value => converter((TEnum)value))
            .ToList();
    }
}
