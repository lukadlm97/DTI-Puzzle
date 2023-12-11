using DTI.Puzzle.Domain.Abstractions.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Contracts.Errors.Validation
{
    public static partial class ValidationErrors
    {
        public static class GlossaryItem
        {
            public static Error ItemNotExist => new Error("GlossaryItem.ItemNotExist", "The glossary item not exist.");
            public static Error TermExist => new Error("GlossaryItem.TermExist", "The term exist at app.");
        }
    }
}
