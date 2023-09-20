using System;

namespace SouthBasement.Helpers
{
    public static class EnumHelper
    {
        public static TEnum[] GetAllValues<TEnum>()
            where TEnum : Enum
            => (TEnum[])Enum.GetValues(typeof(TEnum));
    }
}