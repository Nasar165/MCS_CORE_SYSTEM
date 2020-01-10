using System.Reflection;

namespace mcs.Components
{
    public class ReflectionHelper
    {
        public static PropertyInfo[] GetPropertiesOfObject(object obj)
            => obj.GetType().GetProperties();
    }
}