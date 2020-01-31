using Components.Logger.Interface;

namespace Components.Logger
{
    public class Error : IError
    {
        public int Core_System_Id { get; set; }
        public string E_Date { get; set; }
        public string E_Class { get; set; }
        public string E_Method { get; set; }
        public int E_Row { get; set; }
        public string E_Message { get; set; }
    }
}