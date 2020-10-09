using System;
using System.Linq;
using System.Reflection;

namespace AO.Models.Static
{
    public static class ReflectionHelper
    {
        public static bool HasAttribute<T>(this MemberInfo memberInfo) where T : Attribute
        {
            return HasAttribute<T>(memberInfo, out _);
        }

        public static bool HasAttribute<T>(this MemberInfo memberInfo, out T attribute) where T : Attribute
        {
            var attr = memberInfo.GetCustomAttribute(typeof(T));
            if (attr != null)
            {
                attribute = attr as T;
                return true;
            }

            attribute = null;
            return false;
        }

        public static T GetAttribute<T>(this MemberInfo memberInfo) where T : Attribute
        {
            if (HasAttribute(memberInfo, out T result)) return result;
            return null;
        }

        public static bool HasProperty(this Type type, string propertyName, out PropertyInfo propertyInfo)
        {
            var properties = type.GetProperties().ToDictionary(pi => pi.Name);
            propertyInfo = (properties.ContainsKey(propertyName)) ? properties[propertyName] : null;
            return (propertyInfo != null);
        }

        public static bool Implements(this Type type, Type interfaceType)
        {
            return type.GetInterfaces().Contains(interfaceType);
        }

        public static bool ImplementsAny(this Type type, params Type[] interfaceTypes)
        {
            return type.GetInterfaces().Intersect(interfaceTypes).Any();
        }
    }
}
