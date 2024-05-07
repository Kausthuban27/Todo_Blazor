using AutoMapper;
using TodoAPI.Model;
using TodoAPI.ViewModel;

namespace TodoAPI.Helper
{
    public class Mapper : Profile
    {
        public Mapper() 
        {
            CreateMap<UserData, User>();
            CreateMap<ViewModel.TodoData, Todo>();
        }
    }
}
