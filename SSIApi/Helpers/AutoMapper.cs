using AutoMapper;
using SSIApi.Dto;
using SSIApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSIApi.Helpers
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<User, UserDto>();
      CreateMap<UserDto, User>();
      CreateMap<Article, ArticleDto>();
      CreateMap<ArticleDto, Article>();
      CreateMap<Location, LocationDto>();
      CreateMap<LocationDto, Location>();
      CreateMap<StockItem, StockItemDto>();
      CreateMap<StockItemDto, StockItem>();
    }
  }
}