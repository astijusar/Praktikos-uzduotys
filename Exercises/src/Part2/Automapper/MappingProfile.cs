using AutoMapper;
using Part1;
using Part1.Interfaces;
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
            CreateMap<CfgFile, FileModelDto>()
                .ForMember(
                    dest => dest.FileName,
                    opt => opt.MapFrom(src => src.Name)
                );

            CreateMap<ComparisonResultEntry, ComparisonResultDto>();
        }
    }
}
