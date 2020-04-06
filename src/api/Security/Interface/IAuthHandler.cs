using xAuth.Interface;

namespace api.Security.Interface
{
    public interface IAuthHandler
    {
        ITokenRespons UserAuthentication(IUser user);
        ITokenRespons UserRefreshToken(string refreshToken);
        ITokenRespons TokenAuthentication(IToken tokenKey);
        ITokenRespons TokenRefreshToken(string refreshToken);
    }
}