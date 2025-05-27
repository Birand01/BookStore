using AutoMapper;
using BackEnd.Models;
using BackEnd.Repositories.Contracts;
using BackEnd.Services.Contracts;
using Microsoft.AspNetCore.Identity;

namespace BackEnd.Services.Managers
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _bookService = new Lazy<IBookService>
            (() => new BookManager(manager, logger, mapper));
            _authenticationService = new Lazy<IAuthenticationService>
            (() => new AuthenticationManager(logger, mapper, userManager, configuration));
        }

        public IBookService BookService => _bookService.Value;

        public IAuthenticationService AuthenticationService => throw new NotImplementedException();

    }
}