using BackEnd.DTO;
using BackEnd.Models;
using AutoMapper;

namespace BackEnd.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDtoForUpdate, Book>();
        }
    }
}