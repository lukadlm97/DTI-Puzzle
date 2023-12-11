using AutoMapper;
using DTI.Puzzle.Application.Contracts;
using DTI.Puzzle.Application.Contracts.Errors.Validation;
using DTI.Puzzle.Application.Features.GlossaryItem.Commands.AddGlossaryItem;
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

namespace DTI.Puzzle.Application.UnitTests.Features.GlossaryItem.Commands.AddGlossaryItem
{
    public class AddGlossaryItemTest
    {
        private readonly Mock<IGlossaryItemRepository> _glossaryItemRepositoryMock;
        private readonly Mock<IHistoryChangeRepository> _historyChangesRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<AddGlossaryItemCommandHandler>> _loggerMock;
        public AddGlossaryItemTest()
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

            _loggerMock = new Mock<ILogger<AddGlossaryItemCommandHandler>>();
        }
        [Fact]
        public async Task AddGlossaryItem_Success()
        {
            var handler = new AddGlossaryItemCommandHandler(_unitOfWorkMock.Object, _mapper, _loggerMock.Object);

            var result = await handler.Handle(new AddGlossaryItemCommand("biome", "A large naturally occurring community of flora and fauna occupying a major habitat."), CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Value.Term.Should().Be("biome");
            result.Value.Definition.Should().Be("A large naturally occurring community of flora and fauna occupying a major habitat.");
            result.Value.Id.Should().Be(5);
        }
        [Theory]
        [InlineData("","text")]
        public async Task AddGlossaryItem_Failure_EmptyTerm(string term, string definition)
        {
            var handler = new AddGlossaryItemCommandHandler(_unitOfWorkMock.Object, _mapper, _loggerMock.Object);

            var result = await handler.Handle(new AddGlossaryItemCommand(term, definition), CancellationToken.None);

            result.IsSuccess.Should().Be(false);
            result.Error.Code.Should().Be(Domain.DomainErrors.DomainErrors.GlossaryItem.GlossaryItemModel);
        }

        [Theory]
        [InlineData("newTerm", null)]
        public async Task AddGlossaryItem_Failure_EmptyDefinition(string term, string definition)
        {
            var handler = new AddGlossaryItemCommandHandler(_unitOfWorkMock.Object, _mapper, _loggerMock.Object);

            var result = await handler.Handle(new AddGlossaryItemCommand(term, definition), CancellationToken.None);

            result.IsSuccess.Should().Be(false);
            result.Error.Code.Should().Be(Domain.DomainErrors.DomainErrors.GlossaryItem.GlossaryItemModel);
        }

        [Theory]
        [InlineData("biodiversity", "text")]
        [InlineData("igneous rock", "text")]
        [InlineData("abyssal plain", "text")]
        public async Task AddGlossaryItem_Failure_ExistingTerm(string term, string definition)
        {
            var handler = new AddGlossaryItemCommandHandler(_unitOfWorkMock.Object, _mapper, _loggerMock.Object);

            var result = await handler.Handle(new AddGlossaryItemCommand(term, definition), CancellationToken.None);

            result.IsSuccess.Should().Be(false);
            result.Error.Code.Should().Be(ValidationErrors.GlossaryItem.TermExist);
        }

   

        [Theory]
        [InlineData("sustainable development", "text")]
        public async Task AddGlossaryItem_Success_AddPreviouseExistedTerm(string term, string definition)
        {
            var handler = new AddGlossaryItemCommandHandler(_unitOfWorkMock.Object, _mapper, _loggerMock.Object);

            var result = await handler.Handle(new AddGlossaryItemCommand(term, definition), CancellationToken.None);

            result.IsSuccess.Should().Be(true);
            result.Value.Term.Should().Be("sustainable development");
            result.Value.Definition.Should().Be("text");
            result.Value.Id.Should().Be(5);
        }
    }
}
