using AutoMapper;
using DemoTask.DAL.Models;
using DemoTask.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTask.Common.MappingProfiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, PlayerDto>().ReverseMap();
        }
    }
}
