using DTI.Puzzle.Domain.Abstractions.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Contracts.Errors
{
    public static partial class UnknowErrors
    {
        public static class GlossaryItem
        {
            public static Error UnknowState => new Error("GlossaryItem.UnknowState", "Unable to perform execution of this operation, please contact us!");
        }
    }
}
