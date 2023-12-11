using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.DTOs
{
    public record GlossaryItemDto(int Id, string Term, string Definition);
    public record GlossaryItemPaging(int TotalAvailabeItems, IEnumerable<GlossaryItemDto> GlossaryItems);
}
