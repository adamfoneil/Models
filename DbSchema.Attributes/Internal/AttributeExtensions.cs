using System;
using System.Linq;
using System.Reflection;

namespace AO.DbSchema.Attributes.Internal
{
    internal static class AttributeExtensions
    {
        internal static T GetAttribute<T>(this ICustomAttributeProvider provider) where T : Attribute
        {
            var attrs = provider.GetCustomAttributes(typeof(T), true).OfType<T>();
            return (attrs?.Any() ?? false) ? attrs.FirstOrDefault() : null;
        }

        internal static bool HasAttribute<T>(this ICustomAttributeProvider provider, out T attribute) where T : Attribute
        {
            attribute = GetAttribute<T>(provider);
            return (attribute != null);
        }
    }
}
