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
    public static class MockDictionaryChangesHistoryRepository
    {
        public static Mock<IHistoryChangeRepository> GetIDictionaryChangeHistoryRepository()
        {
            var mockRepository = new Mock<IHistoryChangeRepository>();
            var list = new List<HistoryChange>()
            {
                new HistoryChange()
                {
                    Id = 1,
                    ActionId = 1,
                    GlossaryItemId = 1,
                    DateOfChanges= DateTime.UtcNow.AddDays(1)
                },
                new HistoryChange()
                {
                    Id = 2,
                    ActionId = 1,
                    GlossaryItemId = 2,
                    DateOfChanges= DateTime.UtcNow.AddDays(1)
                },
                new HistoryChange()
                {
                    Id = 3,
                    ActionId = 1,
                    GlossaryItemId = 3,
                    DateOfChanges= DateTime.UtcNow.AddDays(1)
                },
                new HistoryChange()
                {
                    Id = 4,
                    ActionId = 1,
                    GlossaryItemId = 4,
                    DateOfChanges= DateTime.UtcNow.AddDays(1)
                },
                new HistoryChange()
                {
                    Id = 5,
                    ActionId = 3,
                    GlossaryItemId = 4,
                    DateOfChanges= DateTime.UtcNow.AddDays(1)
                }
            };
            mockRepository.Setup(r => r.GetAll())
                .Returns(list.AsQueryable());

            mockRepository.Setup(r => r.Add(It.IsAny<HistoryChange>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((HistoryChange item, CancellationToken cancellationToken) =>
                {
                    list.Add(item);
                    item.Id = list.Count();
                    return item;
                });

            mockRepository.Setup(r => r.GetGlossaryItemChanges(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((int glossaryId, CancellationToken cancellationToken = default) =>
                {
                    return list.Where(x => x.GlossaryItemId == glossaryId).ToList();
                });
            return mockRepository;
        }
    }
}
