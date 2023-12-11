using AutoMapper;
using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItems;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.SearchGlossaryItems;
using DTI.Puzzle.Application.UnitTests.Mocks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.UnitTests.Features.GlossaryItem.Queries.SearchGlossaryItems
{
    public class SearchGlossaryItemsTest
    {
        private readonly Mock<IGlossaryItemRepository> _glossaryItemRepositoryMock;
        private readonly Mock<IHistoryChangeRepository> _historyChangesRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<SearchGlossaryItemsQueryHandler>> _loggerMock;
        public SearchGlossaryItemsTest()
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
            _loggerMock = new Mock<ILogger<SearchGlossaryItemsQueryHandler>>();
        }

        [Fact]
        public async Task SearchGlossaryItems()
        {
            var handler = new SearchGlossaryItemsQueryHandler(_unitOfWorkMock.Object, _mapper, _loggerMock.Object);

            var result = await handler.Handle(new SearchGlossaryItemsQuery(1, 10, "a"), CancellationToken.None);

            result.HasNoValue.Should().Be(false);
            result.Value.GlossaryItems.Any().Should().Be(true);
            result.Value.GlossaryItems.Count().Should().Be(1);
        }

        [Theory]
        [InlineData("igneous")]
        [InlineData("igNEous")]
        [InlineData("biodiversity")]
        [InlineData("biodiVErsity")]
        public async Task SearchGlossaryItems_ByExactWord(string searchCriteria)
        {
            var handler = new SearchGlossaryItemsQueryHandler(_unitOfWorkMock.Object, _mapper, _loggerMock.Object);

            var result = await handler.Handle(new SearchGlossaryItemsQuery(1, 10, searchCriteria), CancellationToken.None);

            result.HasNoValue.Should().Be(false);
            result.Value.GlossaryItems.Any().Should().Be(true);
            result.Value.GlossaryItems.Count().Should().Be(1);
        }


        [Theory]
        [InlineData(0, 0, "a")]
        [InlineData(0, 10, "a")]
        [InlineData(1, 0, "a")]
        [InlineData(-1, 10, "a")]
        [InlineData(1, -1, "a")]
        [InlineData(1, 10, "")]
        [InlineData(1, 10, null)]
        public async Task SearchGlossaryItems_HasNoValue(int pageNumber, int pageSize, string searchCriteria)
        {
            var handler = new SearchGlossaryItemsQueryHandler(_unitOfWorkMock.Object, _mapper, _loggerMock.Object);

            var result = await handler.Handle(new SearchGlossaryItemsQuery(pageNumber, pageSize, searchCriteria), CancellationToken.None);

            result.HasNoValue.Should().Be(true);
        }
        [Theory]
        [InlineData(2, 10)]
        [InlineData(10, 10)]
        public async Task SearchGlossaryItems_HasValue_EmptyCollection(int pageNumber, int pageSize)
        {
            var handler = new SearchGlossaryItemsQueryHandler(_unitOfWorkMock.Object, _mapper, _loggerMock.Object);

            var result = await handler.Handle(new SearchGlossaryItemsQuery(pageNumber, pageSize, "a"), CancellationToken.None);

            result.HasNoValue.Should().Be(false);
            result.Value.GlossaryItems.Any().Should().Be(false);
        }


    }
}
