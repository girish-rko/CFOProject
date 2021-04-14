using AutoMapper;
using CfO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CFOProject.Identity
{
    public class IdentityUserAspNetUserMapProfile : Profile
    {
        protected  void Configure()
        {
            CreateMap<IdentityUser, User>().ReverseMap();
            CreateMap<User, IdentityUser>().ReverseMap();
        }
    }
}