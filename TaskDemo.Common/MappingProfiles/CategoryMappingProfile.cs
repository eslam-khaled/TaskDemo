using AutoMapper;
using DemoTask.DAL.Models;
using DemoTask.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTask.Common.MappingProfiles
{
    class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, TeamDto>();
        }
    }
}
