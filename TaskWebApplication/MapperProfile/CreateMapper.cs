using AutoMapper;
using TaskWebApplication.Models;

namespace TaskWebApplication.MapperProfile
{
    public class CreateMapper : Profile
    {
        public CreateMapper()
        {
            CreateMap<ToDoListItemsDto,ConsumeApiDto>().ReverseMap();   
        }
    }
}
