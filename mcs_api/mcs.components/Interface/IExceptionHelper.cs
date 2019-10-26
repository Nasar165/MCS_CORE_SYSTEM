namespace mcs.components.Interface
{
    public interface IExceptionHelper
    {
        int GetRowThatTrewException();
        string GetMethodThatTrewException();
        string GetClassThatTrewException();
        string GetMessage();
        string GetFormatedErrorMessage();
        Error GetFormatedErrorObject();

    }
}