using AutoMapper;
using DTI.Puzzle.Application.DTOs;
using DTI.Puzzle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GlossaryItem, GlossaryItemDto>();
            CreateMap<HistoryChange, HistoryChangesDto>()
                .ConstructUsing(src=> new HistoryChangesDto(src.Id, src.ActionId??0, src.DateOfChanges));
        }
    }
}
