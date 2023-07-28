using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models.DTOs;
using Core.Models;

namespace Core.mapper
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

            CreateMap<ComparisonResultEntry, ComparisonResultDto>();
        }
    }
}
