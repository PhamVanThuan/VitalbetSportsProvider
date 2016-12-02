namespace VitalbetSportsProvider.Core
{
    using System;

    public static class EnumEx
    {
        public static T ToEnum<T>(this string item) 
            where T : struct, IConvertible
        {
            return (T)Enum.Parse(typeof(T), item, true);
        }

        public static string ToEnumName<T>(this int id)
            where T : struct, IConvertible
        {
            return Enum.GetName(typeof(T), id);
        }
    }
}
