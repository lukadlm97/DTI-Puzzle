using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Application.DTOs;
using DTI.Puzzle.Domain.Abstractions.OperationResult;
using DTI.Puzzle.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.Utilities
{
    public static class CollectionExtension
    {
        public static bool IsExactTermInGlossary(this IQueryable<GlossaryItem> collection, string term)
        {
            var itemsWithNewTerm = collection
                                   .Where(x => x.Term.ToLower().Contains(term.ToLower()) && x.IsActive)
                                   .ToList();
            if (!itemsWithNewTerm.Any())
            {
                return false;
            }
            return itemsWithNewTerm.Any(item => item.Term.ToLower() == term.ToLower());
        }
        public static bool IsExactOneTermInGlossary(this IQueryable<GlossaryItem> collection, string term)
        {
            var itemsWithNewTerm = collection
                                   .Where(x => x.Term.ToLower().Contains(term.ToLower()) && x.IsActive)
                                   .ToList();
            if (!itemsWithNewTerm.Any())
            {
                return false;
            }
            return itemsWithNewTerm.Count(item => item.Term.ToLower() == term.ToLower()) > 1;
        }
    }
}
