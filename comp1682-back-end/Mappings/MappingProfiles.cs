using AutoMapper;
using comp1682_back_end.DTOs;
using comp1682_back_end.Models;

namespace comp1682_back_end.Mappings
{

  public class MappingProfiles : Profile
  {
    public MappingProfiles()
    {
      CreateMap<Product, ProductDto>().ReverseMap();
      CreateMap<Category, CategoryDto>().ReverseMap();
    }
  }
}
