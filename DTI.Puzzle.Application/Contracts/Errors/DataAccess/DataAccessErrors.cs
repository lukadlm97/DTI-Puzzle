using DTI.Puzzle.Domain.Abstractions.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Contracts.Errors.DataAccess
{
    public static partial class DataAccessErrors
    {
        public static class GlossaryItem
        {
            internal static Error ExecutionProblem => new Error("GlossaryItem.ExecutionProblem", "Some errors occured on execution operation at data access level");
        }
    }
}
