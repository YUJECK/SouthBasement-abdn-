using System;

namespace TheRat.Helpers
{
    public static class EnumHelper
    {
        public static TEnum[] GetAllDirections<TEnum>()
            where TEnum : Enum
            => (TEnum[])Enum.GetValues(typeof(TEnum));
    }
}