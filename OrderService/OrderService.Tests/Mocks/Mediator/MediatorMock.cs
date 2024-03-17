using Azure.Core;
using Azure;
using MediatR;
using Moq;
using OrderService.Application.DTOs.Order;
using OrderService.Application.MediatR.Order.Queries;
using OrderService.Application.MediatR.Order.Commands;

namespace OrderService.Tests.Mocks.Mediator
{
    public class MediatorMock : Mock<IMediator>
    {
        public MediatorMock MockSendGetAllOrdersQuery(GetAllOrdersQuery request,
            ICollection<ReadOrderDTO> response)
        {
            Setup(mediator => mediator.Send(It.Is<GetAllOrdersQuery>(
                query => query.SkipCount == request.SkipCount &&
                query.SelectionAmount == request.SelectionAmount), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response)
            .Verifiable();

            return this;
        }

        public MediatorMock MockSendGetOrderByIdQuery(GetOrderByIdQuery request,
            ReadOrderDTO response)
        {
            Setup(mediator => mediator.Send(It.Is<GetOrderByIdQuery>(
                query => query.Id == request.Id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response)
            .Verifiable();

            return this;
        }

        public MediatorMock MockSendInsertOrderCommand(InsertOrderCommand request,
            ReadOrderDTO response)
        {
            Setup(mediator => mediator.Send(It.Is<InsertOrderCommand>(command =>
            command.MenuId == request.MenuId && request.BookingId == command.BookingId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response)
            .Verifiable();

            return this;
        }

        public MediatorMock MockSendUpdateOrderCommand(UpdateOrderCommand request,
            ReadOrderDTO response)
        {
            Setup(mediator => mediator.Send(It.Is<UpdateOrderCommand>(command =>
            command.MenuId == request.MenuId && command.Id == request.Id),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response)
            .Verifiable();

            return this;
        }

        public MediatorMock MockSendDeleteOrderCommand(int id)
        {
            Setup(mediator => mediator.Send(It.Is<DeleteOrderCommand>(command =>
            command.Id == id), It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }
    }
}
