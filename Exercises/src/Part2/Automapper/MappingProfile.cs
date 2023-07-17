using AutoMapper;
using Part2.Models;
using Part2.Models.DTOs;

namespace Part2.Automapper
{
    /// <summary>
    /// A class to configure automapper mapping
    /// </summary>
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
