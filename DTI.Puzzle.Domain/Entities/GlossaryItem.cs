using DTI.Puzzle.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Domain.Entities
{
    public class GlossaryItem : BaseEntity
    {
        public string Term { get; set; }
        public string Definition { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
