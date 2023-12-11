using DTI.Puzzle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Persistence
{
    public class GlossaryInMemoryDbContext
    {
        public List<Domain.Entities.Action> Actions = new List<Domain.Entities.Action>() 
        { 
            new Domain.Entities.Action()
            {
                Id = 1,
                Name = nameof(Domain.Enums.ActionEnum.Create),
            },
            new Domain.Entities.Action()
            {
                Id = 2,
                Name = nameof(Domain.Enums.ActionEnum.Update),
            },
            new Domain.Entities.Action()
            {
                Id = 3,
                Name = nameof(Domain.Enums.ActionEnum.Delete),
            }
        };

        public List<GlossaryItem> GlossaryItems = new List<GlossaryItem>()
        {
            new GlossaryItem()
            {
                Id = 1,
                Term = "abyssal plain",
                Definition = "The ocean floor from the continental margin, usually very flat with a slight slope.",
                IsActive = true,
            },
            new GlossaryItem()
            {
                Id = 2,
                Term = "igneous rock",
                Definition = "A type of rock that forms from the cooling and solidification of magma or lava.",
                IsActive = true,
            },
            new GlossaryItem()
            {
                Id = 3,
                Term = "biodiversity",
                Definition = "The variety of plant and animal life in a particular habitat, a high level of which is usually considered to be important and desirable.",
                IsActive = true,
            },
            new GlossaryItem()
            {
                Id = 4,
                Term = "sustainable development",
                Definition = "Development that meets the needs of the present without compromising the ability of future generations to meet their own needs.",
                IsActive = false,
            }
        };

        public List<HistoryChange> DictionaryChangeHistories = new List<HistoryChange>() 
        {
            new HistoryChange()
            {
                Id = 1,
                ActionId = 1,
                GlossaryItemId = 1,
            },
            new HistoryChange()
            {
                Id = 2,
                ActionId = 1,
                GlossaryItemId = 2,
            },
            new HistoryChange()
            {
                Id = 3,
                ActionId = 1,
                GlossaryItemId = 3,
            },
            new HistoryChange()
            {
                Id = 4,
                ActionId = 1,
                GlossaryItemId = 4,
            },
            new HistoryChange()
            {
                Id = 5,
                ActionId = 3,
                GlossaryItemId = 4,
            }
        };

    }
}
