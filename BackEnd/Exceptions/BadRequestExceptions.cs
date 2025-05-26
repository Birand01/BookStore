namespace BackEnd.Exceptions
{
    public class BadRequestExceptions:Exception
    {
        protected BadRequestExceptions(string message):base(message)
        {
        }
    }
}