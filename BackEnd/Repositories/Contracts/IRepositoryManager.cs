namespace BackEnd.Repositories.Contracts
{
    public interface IRepositoryManager
    {
         IBookRepository Book {get;}
         Task SaveAsync();
    }
}