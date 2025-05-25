using BackEnd.DTO;
using BackEnd.Models;
using AutoMapper;

namespace BackEnd.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDtoForInsertion, Book>();
            CreateMap<BookDtoForUpdate, Book>();
        }
    }
}