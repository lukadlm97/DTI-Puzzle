using DTI.Puzzle.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Domain.Entities
{
    public class HistoryChange : BaseEntity
    {
        public virtual GlossaryItem? GlossaryItem { get; set; }
        public int? GlossaryItemId { get; set; }
        public virtual Action? Action { get; set; }
        public short? ActionId { get; set; }
        public DateTime DateOfChanges { get; set; }
    }
}
