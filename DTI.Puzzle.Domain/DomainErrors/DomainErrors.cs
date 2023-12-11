using DTI.Puzzle.Domain.Abstractions.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Domain.DomainErrors
{
    public static partial class DomainErrors
    {
        public static class GlossaryItem
        {
            public static Error GlossaryItemModel => new Error(
              "GlossaryItem.GlossaryItemModel",
              "Some of provided values dont satisfy condition.");
        }
    }
}
