using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.UnitTests.Mocks
{
    public static class MockGlossaryItemRepository
    {
        public static Mock<IGlossaryItemRepository> GetGlossaryItemRepository()
        {
            var list = new List<GlossaryItem>()
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
            var mockRepository = new Mock<IGlossaryItemRepository>();

            mockRepository.Setup(r => r.GetAll())
                .Returns(list.Where(x=>x.IsActive).AsQueryable());

            mockRepository.Setup(r => r.Add(It.IsAny<GlossaryItem>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((GlossaryItem glossaryItem,CancellationToken cancellationToken) =>
                {
                    list.Add(glossaryItem);
                    glossaryItem.Id = list.Count();
                    return glossaryItem;
                });

            mockRepository.Setup(r => r.Get(It.IsAny<int>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync((int glossaryItemId, CancellationToken cancellationToken) =>
               {
                   return list.FirstOrDefault(x => x.Id == glossaryItemId && x.IsActive);
               });

            mockRepository.Setup(r => r.Exists(It.IsAny<int>(), It.IsAny<CancellationToken>()))
              .ReturnsAsync((int glossaryItemId, CancellationToken cancellationToken) =>
              {
                  return list.Any(x => x.Id == glossaryItemId && x.IsActive);
              });

            mockRepository.Setup(r => r.Delete(It.IsAny<GlossaryItem>()))
                .Callback((GlossaryItem item) =>
            {
                var existingItem = list.FirstOrDefault(x => x.Id == item.Id);
                existingItem.IsActive = false;
            });
            //mockRepository.Verify(r => r.Delete(It.IsAny<GlossaryItem>()), Times.Once()); ;

            mockRepository.Setup(r => r.Update(It.IsAny<GlossaryItem>()))
                .Callback((GlossaryItem item) =>
                {
                    var existingItem = list.FirstOrDefault(x => x.Id == item.Id);
                    list.Remove(existingItem);
                    list.Add(item);
                });
          //  mockRepository.Verify(r => r.Update(It.IsAny<GlossaryItem>()), Times.Once());

            mockRepository.Setup(r=>r.ExistTerm(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((string searchTerm, CancellationToken cancellationToken) =>
                {
                    return list.Where(x => x.Term.ToLower().Contains(searchTerm.ToLower()) && x.IsActive).ToList();
                });
            return mockRepository;
        }
    }
}
