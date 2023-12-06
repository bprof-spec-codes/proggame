using AutoMapper;
using proggame.Entities;
using proggame.Services.Dtos.TestFileDtos;

namespace proggame.ObjectMapping;

public class proggameAutoMapperProfile : Profile
{
    public proggameAutoMapperProfile()
    {
        CreateMap<TestFile, TestFileDto>();
        CreateMap<TestFileDto, TestFile>();
    }
}
