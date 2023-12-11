using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.DTOs
{
    public record HistoryChangesDto(int Id, short ActionId, DateTime DateOfCreation);
}
