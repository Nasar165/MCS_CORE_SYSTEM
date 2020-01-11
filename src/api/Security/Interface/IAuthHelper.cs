using api.Security.AuthTemplate.Interface;

namespace api.Security.Interface
{
    public interface IAuthHelper
    {
        object AuthenticateUser(IUserAccount user, AuthAction method);

        object AuthentiacteAPI(IAccessKey ApiKey);
    }
}