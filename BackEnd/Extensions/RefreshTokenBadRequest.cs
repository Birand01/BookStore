using BackEnd.Exceptions;

namespace BackEnd.Extensions
{
    public class RefreshTokenBadRequest:BadRequestExceptions
    {
        public RefreshTokenBadRequest():base("Invalid client request. The token has expired on its own.")
        {

        }
    }
}