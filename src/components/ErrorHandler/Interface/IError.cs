namespace Components.Errorhandler.Interface
{
    public interface IError
    {
        int Core_System_Id { get; set; }
        string E_Date { get; set; }
        string E_Class { get; set; }
        string E_Method { get; set; }
        string E_Message { get; set; }
    }
}