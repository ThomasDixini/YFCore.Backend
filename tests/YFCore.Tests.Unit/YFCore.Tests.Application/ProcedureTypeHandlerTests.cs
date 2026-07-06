using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using YFCore.Application.Contracts;
using YFCore.Application.ProcedureTypes.Commands.CreateProcedureType;
using YFCore.Application.ProcedureTypes.Commands.UpdateProcedureTypeCommand;
using YFCore.Application.ProcedureTypes.Queries.GetProcedureTypeById;
using YFCore.Application.ProcedureTypes.Queries.GetProcedureTypes;
using YFCore.Application.ProcedureTypes.Contracts;
using YFCore.Domain.ProcedureTypes.Entity;
using YFCore.Domain.ProcedureTypes.Repository;
using YFCore.Domain.Shared.Exceptions;

namespace YFCore.Tests.Unit.YFCore.Tests.Application
{
    public class ProcedureTypeHandlerTests
    {
        [Fact]
        public async Task CreateProcedureTypeCommandHandler_ShouldAddAndCommit()
        {
            var repository = new Mock<IProcedureTypeRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var handler = new CreateProcedureTypeCommandHandler(repository.Object, unitOfWork.Object);
            var result = await handler.Handle(new CreateProcedureTypeCommand("Consultation", "A consultation."), CancellationToken.None);

            repository.Verify(r => r.Add(It.IsAny<ProcedureType>()), Times.Once);
            unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateProcedureTypeCommandHandler_ShouldUpdateAndCommit()
        {
            var procedureType = new ProcedureType("Consultation", "A consultation.");
            var repository = new Mock<IProcedureTypeRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            repository.Setup(r => r.GetByIdAsync(procedureType.Id)).ReturnsAsync(procedureType);
            unitOfWork.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var handler = new UpdateProcedureTypeCommandHandler(repository.Object, unitOfWork.Object);
            var request = new UpdateProcedureTypeCommand(procedureType.Id, "Surgery", "A surgery procedure.", true);
            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().BeEquivalentTo(new { Id = procedureType.Id, Name = "SURGERY", Description = "A SURGERY PROCEDURE." });
            unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateProcedureTypeCommandHandler_ShouldThrow_WhenNotFound()
        {
            var repository = new Mock<IProcedureTypeRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((ProcedureType?)null);

            var handler = new UpdateProcedureTypeCommandHandler(repository.Object, unitOfWork.Object);
            Func<Task> act = async () => await handler.Handle(new UpdateProcedureTypeCommand(Guid.NewGuid(), "Name", "Description", true), CancellationToken.None);

            await act.Should().ThrowAsync<DomainException>().WithMessage("Procedure type not found");
        }

        [Fact]
        public async Task GetProcedureTypeByIdHandler_ShouldReturnDto_WhenFound()
        {
            var expected = new ProcedureTypeDTO(Guid.NewGuid(), "TEST", "DESCRIPTION");
            var procedureTypeRead = new Mock<IProcedureTypeRead>();
            procedureTypeRead.Setup(r => r.GetByIdAsync(expected.Id)).ReturnsAsync(expected);

            var handler = new GetProcedureTypeByIdHandler(procedureTypeRead.Object);
            var result = await handler.Handle(new GetProcedureTypeByIdQuery(expected.Id), CancellationToken.None);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetProcedureTypeByIdHandler_ShouldReturnNull_WhenNotFound()
        {
            var procedureTypeRead = new Mock<IProcedureTypeRead>();
            procedureTypeRead.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((ProcedureTypeDTO?)null);

            var handler = new GetProcedureTypeByIdHandler(procedureTypeRead.Object);
            var result = await handler.Handle(new GetProcedureTypeByIdQuery(Guid.NewGuid()), CancellationToken.None);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetProcedureTypesHandler_ShouldReturnList()
        {
            var expected = new[] { new ProcedureTypeDTO(Guid.NewGuid(), "TEST", "DESCRIPTION") };
            var procedureTypeRead = new Mock<IProcedureTypeRead>();
            procedureTypeRead.Setup(r => r.GetProcedureTypesAsync()).ReturnsAsync(expected);

            var handler = new GetProcedureTypesHandler(procedureTypeRead.Object);
            var result = await handler.Handle(new GetProcedureTypesQuery(), CancellationToken.None);

            result.Should().BeEquivalentTo(expected);
        }
    }
}
