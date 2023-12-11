using AutoMapper;
using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItem;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItemHistory;
using DTI.Puzzle.Application.UnitTests.Mocks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.UnitTests.Features.GlossaryItem.Queries.GetGlossaryItemHistory
{
    public class GetGlossaryItemHistoryTest
    {
        private readonly Mock<IGlossaryItemRepository> _glossaryItemRepositoryMock;
        private readonly Mock<IHistoryChangeRepository> _historyChangesRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<GetGlossaryItemHistoryQueryHandler>> _loggerMock;
        public GetGlossaryItemHistoryTest()
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
            _loggerMock = new Mock<ILogger<GetGlossaryItemHistoryQueryHandler>>();
        }
        [Fact]
        public async Task GetGlossaryItemHistory()
        {
            var handler = new GetGlossaryItemHistoryQueryHandler(_unitOfWorkMock.Object, 
                                                                    _mapper, 
                                                                    _loggerMock.Object);

            var result = await handler.Handle(new GetGlossaryItemHistoryQuery(1), CancellationToken.None);

            result.HasNoValue.Should().Be(false);
            result.Value.First().Id.Should().Be(1);
            result.Value.First().ActionId.Should().Be(1);
        }
        [Fact]
        public async Task GetGlossaryItemHistory_WithTwohistoryItems()
        {
            var handler = new GetGlossaryItemHistoryQueryHandler(_unitOfWorkMock.Object, 
                                                                    _mapper,
                                                                    _loggerMock.Object);

            var result = await handler.Handle(new GetGlossaryItemHistoryQuery(4), CancellationToken.None);

            result.HasValue.Should().BeTrue();
            result.Value.Count().Should().Be(2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(100)]
        public async Task GetGlossaryItemHistory_HasNoValue(int id)
        {
            var handler = new GetGlossaryItemHistoryQueryHandler(_unitOfWorkMock.Object, 
                                                                    _mapper,
                                                                    _loggerMock.Object);

            var result = await handler.Handle(new GetGlossaryItemHistoryQuery(id), CancellationToken.None);

            result.HasNoValue.Should().Be(true);
        }


    }
}
