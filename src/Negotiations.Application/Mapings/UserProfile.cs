using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Negotiations.Application.Models;
using Negotiations.Domain.Entities;

namespace Negotiations.Application.Mapings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LoginUserDto, User>();
            CreateMap<RegisterUserDto, User>();
        }
    }
}