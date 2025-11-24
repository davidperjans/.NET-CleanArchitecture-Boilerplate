using Application.Auth.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Auth.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
