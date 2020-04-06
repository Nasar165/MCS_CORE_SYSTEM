namespace Components
{
    public static class Validation
    {
        public static bool StringsAreEqual(string one, string two)
           => one.ToLower() == two.ToLower() ? true : false;

        public static bool ObjectIsNull(object obj)
            => obj == null ? true : false;

        public static bool ValueIsGreateherThan(int value, int greater)
            => value > greater ? true : false;
    }
}