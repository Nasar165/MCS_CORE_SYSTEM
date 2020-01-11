using System.Reflection;

namespace Components
{
    public class ReflectionHelper
    {
        public static PropertyInfo[] GetPropertiesOfObject(object obj)
            => obj.GetType().GetProperties();
    }
}