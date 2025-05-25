using BackEnd.Services.Contracts;

namespace BackEnd.Services.Contracts
{
    public interface IServiceManager
    {
        IBookService BookService { get; }
    }
}