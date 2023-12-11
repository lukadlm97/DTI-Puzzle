using DTI.Puzzle.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Domain.Entities
{
    public class Action : EnumBaseEntity
    {
        [Required, MaxLength(25)]
        public string Name { get; set; }
    }
}
