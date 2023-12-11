using AutoMapper;
using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItem;
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

namespace DTI.Puzzle.Application.UnitTests.Features.GlossaryItem.Queries.GetGlossaryItem
{
    public class GetGloassaryItemTest
    {
        private readonly Mock<IGlossaryItemRepository> _glossaryItemRepositoryMock;
        private readonly Mock<IHistoryChangeRepository> _historyChangesRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<GetGlossaryItemQueryHandler>> _loggerMock;
        public GetGloassaryItemTest()
        {
            _glossaryItemRepositoryMock = MockGlossaryItemRepository.GetGlossaryItemRepository();
            _historyChangesRepositoryMock = MockDictionaryChangesHistoryRepository.GetIDictionaryChangeHistoryRepository();
            _unitOfWork = MockUnitOfWork
                .GetUnitOfWork(_glossaryItemRepositoryMock.Object, _historyChangesRepositoryMock.Object);
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<Application.Mappings.MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _loggerMock = new Mock<ILogger<GetGlossaryItemQueryHandler>>();
        }

        [Fact]
        public async Task GetGlossaryItem()
        {
            var handler = new GetGlossaryItemQueryHandler(_unitOfWork.Object, _mapper, _loggerMock.Object);

            var result = await handler.Handle(new GetGlossaryItemQuery(1), CancellationToken.None);

            result.HasNoValue.Should().Be(false);
            result.Value.Id.Should().Be(1);
            result.Value.Term.Should().Be("abyssal plain");
            result.Value.Definition.Should().Be("The ocean floor from the continental margin, usually very flat with a slight slope.");
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(100)]
        public async Task GetGlossaryItem_HasNoValue(int id)
        {
            var handler = new GetGlossaryItemQueryHandler(_unitOfWork.Object, _mapper, _loggerMock.Object);

            var result = await handler.Handle(new GetGlossaryItemQuery(id), CancellationToken.None);

            result.HasNoValue.Should().Be(true);
        }
        [Theory]
        [InlineData(4)]
        public async Task GetGlossaryItem_HasNoValue_InActiveTerm(int id)
        {
            var handler = new GetGlossaryItemQueryHandler(_unitOfWork.Object, _mapper, _loggerMock.Object);

            var result = await handler.Handle(new GetGlossaryItemQuery(id), CancellationToken.None);

            result.HasNoValue.Should().Be(true);
        }
    }
}
