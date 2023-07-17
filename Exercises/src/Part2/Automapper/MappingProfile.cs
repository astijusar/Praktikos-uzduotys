using AutoMapper;
using Part2.Models;
using Part2.Models.DTOs;

namespace Part2.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FileModel, FileModelDto>()
                .ForMember(
                    dest => dest.FileName,
                    opt => opt.MapFrom(src => src.Name)
                );

            CreateMap<ComparisonResult, ComparisonResultDto>();
        }
    }
}
