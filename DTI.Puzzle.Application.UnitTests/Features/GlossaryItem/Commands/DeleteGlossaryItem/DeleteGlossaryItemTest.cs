using AutoMapper;
using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Application.Contracts.Errors.Validation;
using DTI.Puzzle.Application.Features.GlossaryItem.Commands.AddGlossaryItem;
using DTI.Puzzle.Application.Features.GlossaryItem.Commands.DeleteGlossaryItem;
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

namespace DTI.Puzzle.Application.UnitTests.Features.GlossaryItem.Commands.DeleteGlossaryItem
{
    public class DeleteGlossaryItemTest
    {
        private readonly Mock<IGlossaryItemRepository> _glossaryItemRepositoryMock;
        private readonly Mock<IHistoryChangeRepository> _historyChangesRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<DeleteGlossaryItemCommandHandler>> _loggerDeleteGlossaryItemCommandHandlerMock;
        private readonly Mock<ILogger<GetGlossaryItemQueryHandler>> _loggerGetGlossaryItemQueryHandlerMock;
        public DeleteGlossaryItemTest()
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
            _loggerDeleteGlossaryItemCommandHandlerMock = new Mock<ILogger<DeleteGlossaryItemCommandHandler>>();
            _loggerGetGlossaryItemQueryHandlerMock = new Mock<ILogger<GetGlossaryItemQueryHandler>>();
        }
        [Fact]
        public async Task DeleteGlossaryItem_Success()
        {
            var handler = new DeleteGlossaryItemCommandHandler(_unitOfWorkMock.Object,          
                                                                _loggerDeleteGlossaryItemCommandHandlerMock.Object);
            var result = await handler.Handle(new DeleteGlossaryItemCommand(1), CancellationToken.None);
            result.IsSuccess.Should().Be(true);


            var handlerGetById = new GetGlossaryItemQueryHandler(_unitOfWorkMock.Object,
                                                                    _mapper, 
                                                                    _loggerGetGlossaryItemQueryHandlerMock.Object);
            var resultGetById = await handlerGetById.Handle(new GetGlossaryItemQuery(1), CancellationToken.None);
            resultGetById.HasValue.Should().Be(false);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task DeleteGlossaryItem_Failure_IdLowerTheZero(int id)
        {
            var handler = new DeleteGlossaryItemCommandHandler(_unitOfWorkMock.Object, 
                                                                _loggerDeleteGlossaryItemCommandHandlerMock.Object);
            var result = await handler.Handle(new DeleteGlossaryItemCommand(id), CancellationToken.None);
            result.IsSuccess.Should().Be(false);
            result.Error.Code.Should().Be(Domain.DomainErrors.DomainErrors.GlossaryItem.GlossaryItemModel);
        }
        [Theory]
        [InlineData(10)]
        public async Task DeleteGlossaryItem_Failure_OutOfRangeId(int id)
        {
            var handler = new DeleteGlossaryItemCommandHandler(_unitOfWorkMock.Object, 
                                                                _loggerDeleteGlossaryItemCommandHandlerMock.Object);
            var result = await handler.Handle(new DeleteGlossaryItemCommand(id), CancellationToken.None);
            result.IsSuccess.Should().Be(false);
            result.Error.Code.Should().Be(ValidationErrors.GlossaryItem.ItemNotExist);
        }
       
    }
}
