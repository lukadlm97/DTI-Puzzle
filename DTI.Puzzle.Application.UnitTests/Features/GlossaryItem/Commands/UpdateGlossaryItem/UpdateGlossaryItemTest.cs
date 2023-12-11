using AutoMapper;
using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Application.Contracts.Errors.Validation;
using DTI.Puzzle.Application.Features.GlossaryItem.Commands.DeleteGlossaryItem;
using DTI.Puzzle.Application.Features.GlossaryItem.Commands.UpdateGlossaryItem;
using DTI.Puzzle.Application.Features.GlossaryItem.Queries.GetGlossaryItem;
using DTI.Puzzle.Application.UnitTests.Mocks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTI.Puzzle.Application.UnitTests.Features.GlossaryItem.Commands.UpdateGlossaryItem
{
    public class UpdateGlossaryItemTest
    {
        private readonly Mock<IGlossaryItemRepository> _glossaryItemRepositoryMock;
        private readonly Mock<IHistoryChangeRepository> _dictionaryChangeHisotryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock ;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<UpdateGlossaryItemCommandHandler>> _loggerUpdateGlossaryItemCommandHandlerMock;
        private readonly Mock<ILogger<GetGlossaryItemQueryHandler>> _loggerGetGlossaryItemQueryHandlerMock;
        public UpdateGlossaryItemTest()
        {
            _glossaryItemRepositoryMock = MockGlossaryItemRepository.GetGlossaryItemRepository();
            _dictionaryChangeHisotryMock = MockDictionaryChangesHistoryRepository.GetIDictionaryChangeHistoryRepository();
            _unitOfWorkMock = MockUnitOfWork
                .GetUnitOfWork(_glossaryItemRepositoryMock.Object, _dictionaryChangeHisotryMock.Object);
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<Application.Mappings.MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _loggerUpdateGlossaryItemCommandHandlerMock = new Mock<ILogger<UpdateGlossaryItemCommandHandler>>();
            _loggerGetGlossaryItemQueryHandlerMock = new Mock<ILogger<GetGlossaryItemQueryHandler>>();
        }
        [Fact]
        public async Task UpdateGlossaryItem_Success()
        {
            var handler = new UpdateGlossaryItemCommandHandler(_unitOfWorkMock.Object, 
                                                                _mapper, 
                                                                _loggerUpdateGlossaryItemCommandHandlerMock.Object);

            var result = await handler.Handle(new UpdateGlossaryItemCommand(1,"term","definition"), CancellationToken.None);

            result.IsSuccess.Should().Be(true);
            result.Value.Id.Should().Be(1);
            result.Value.Term.Should().Be("term");
            result.Value.Definition.Should().Be("definition");
        }
        [Theory]
        [InlineData(-1, "term", "definition")]
        [InlineData(0, "term", "definition")]
        [InlineData(10, "term", "definition")]
        public async Task UpdateGlossaryItem_Failure_NegiativeId(int id, string term, string definition)
        {
            var handler = new UpdateGlossaryItemCommandHandler(_unitOfWorkMock.Object, 
                                                                _mapper, 
                                                                _loggerUpdateGlossaryItemCommandHandlerMock.Object);
            var result = await handler.Handle(new UpdateGlossaryItemCommand(id, term, definition), CancellationToken.None);
            result.IsSuccess.Should().Be(false);
        }
        [Theory]
        [InlineData(10, "term", "definition")]
        public async Task UpdateGlossaryItem_Failure_OutOfRangeId(int id, string term, string definition)
        {
            var handler = new UpdateGlossaryItemCommandHandler(_unitOfWorkMock.Object, 
                                                                _mapper, 
                                                                _loggerUpdateGlossaryItemCommandHandlerMock.Object);
            var result = await handler.Handle(new UpdateGlossaryItemCommand(id, term, definition), CancellationToken.None);
            result.IsSuccess.Should().Be(false);
        }

        [Fact]
        public async Task UpdateGlossaryItem_Failure_UpdateOfInActiveItem()
        {
            var handler = new UpdateGlossaryItemCommandHandler(_unitOfWorkMock.Object,
                                                                _mapper, 
                                                                _loggerUpdateGlossaryItemCommandHandlerMock.Object);
            var result = await handler.Handle(new UpdateGlossaryItemCommand(4, "term", "definition"), CancellationToken.None);
            result.IsSuccess.Should().BeFalse();
            result.Error.Code.Should().Be(ValidationErrors.GlossaryItem.ItemNotExist);
        }
    }
}
