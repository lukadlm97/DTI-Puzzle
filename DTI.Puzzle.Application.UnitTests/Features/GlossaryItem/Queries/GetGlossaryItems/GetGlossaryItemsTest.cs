using AutoMapper;
using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItem;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItems;
using DTI.Puzzle.Application.UnitTests.Mocks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.UnitTests.Features.GlossaryItem.Queries.GetGlossaryItems
{
    public class GetGlossaryItemsTest
    {
        private readonly Mock<IGlossaryItemRepository> _glossaryItemRepositoryMock;
        private readonly Mock<IHistoryChangeRepository> _historyChangesRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<GetGlossaryItemsQueryHandler>> _loggerMock;
        public GetGlossaryItemsTest()
        {
            _glossaryItemRepositoryMock = MockGlossaryItemRepository.GetGlossaryItemRepository();
            _historyChangesRepositoryMock = MockDictionaryChangesHistoryRepository.GetIDictionaryChangeHistoryRepository();
            _unitOfWorkMock = MockUnitOfWork
                .GetUnitOfWork(_glossaryItemRepositoryMock.Object, _historyChangesRepositoryMock.Object);
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<Application.Mappings.MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _loggerMock = new Mock<ILogger<GetGlossaryItemsQueryHandler>>();
        }


        [Fact]
        public async Task GetGlossaryItems()
        {
            var handler = new GetGlossaryItemsQueryHandler(_unitOfWorkMock.Object, _mapper, _loggerMock.Object);

            var result = await handler.Handle(new GetGlossaryItemsQuery(1,10), CancellationToken.None);

            result.HasNoValue.Should().Be(false);
            result.Value.GlossaryItems.Any().Should().Be(true);
            result.Value.GlossaryItems.Count().Should().Be(3);
        }
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 10)]
        [InlineData(1, 0)]
        [InlineData(-1, 10)]
        [InlineData(1, -1)]
        public async Task GetGlossaryItems_HasNoValue(int pageNumber, int pageSize)
        {
            var handler = new GetGlossaryItemsQueryHandler(_unitOfWorkMock.Object, _mapper, _loggerMock.Object);

            var result = await handler.Handle(new GetGlossaryItemsQuery(pageNumber, pageSize), CancellationToken.None);

            result.HasNoValue.Should().Be(true);
        }
        [Theory]
        [InlineData(2, 10)]
        [InlineData(10, 10)]
        public async Task GetGlossaryItems_HasValue_EmptyCollection(int pageNumber, int pageSize)
        {
            var handler = new GetGlossaryItemsQueryHandler(_unitOfWorkMock.Object, _mapper, _loggerMock.Object);

            var result = await handler.Handle(new GetGlossaryItemsQuery(pageNumber, pageSize), CancellationToken.None);

            result.HasNoValue.Should().Be(false);
            result.Value.GlossaryItems.Any().Should().Be(false);
        }
    }
}
