using DTI.Puzzle.Application.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork(IGlossaryItemRepository glossaryItemRepository,
            IHistoryChangeRepository dictionaryChangeHistoryRepository)
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(x => x.GlossaryItemRepository)
                .Returns(glossaryItemRepository);
            mockUnitOfWork.Setup(x => x.ChangeHistoryRepository)
                .Returns(dictionaryChangeHistoryRepository);

            return mockUnitOfWork;
        }
    }
}
