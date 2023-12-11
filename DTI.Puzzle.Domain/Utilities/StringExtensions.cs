using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Domain.Utilities
{
    public static class StringExtensions
    {
        public static bool ConvertBooleanFromConfiguation(this string? configuationValue)
        {
            return bool.TryParse(configuationValue, out bool result) ? result : false;
        }
    }
}
