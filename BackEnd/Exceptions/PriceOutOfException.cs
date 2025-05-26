namespace BackEnd.Exceptions
{
    public class PriceOutOfRangeBadRequestException:BadRequestExceptions
    {
        public PriceOutOfRangeBadRequestException()
        :base("Max price should be less than 1000 and greater than 10")
        {
        }
    }   
}