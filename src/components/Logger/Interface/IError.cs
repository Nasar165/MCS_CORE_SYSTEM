namespace Components.Logger.Interface
{
    public interface IError
    {
        int Core_System_Id { get; set; }
        string E_Date { get; set; }
        string E_Class { get; set; }
        string E_Method { get; set; }

        int E_Row { get; set; }
        string E_Message { get; set; }
    }
}