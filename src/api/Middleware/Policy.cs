namespace api.Middleware
{
    public class Policy
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public bool AddToHeader { get; set; }
    }
}