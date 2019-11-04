using System.Reflection;

namespace mcs.components
{
    public class ReflectionHelper
    {
        public static PropertyInfo[] GetPropertiesOfObject(object obj)
            => obj.GetType().GetProperties();
    }
}