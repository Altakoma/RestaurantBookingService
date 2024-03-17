using Amazon.Runtime.Internal;
using AutoMapper;
using Confluent.Kafka.Admin;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderService.Application.DTOs.Order;
using OrderService.Application.MappingProfiles;
using OrderService.Application.MediatR.Order.Commands;
using OrderService.Application.MediatR.Order.Queries;
using OrderService.Presentation.Controllers;
using OrderService.Tests.Fakers;
using OrderService.Tests.Mocks.Mappers;
using OrderService.Tests.Mocks.Mediator;

namespace OrderService.Tests.ControllerTests
{
    public class OrderControllerTests
    {
        private readonly MapperMock _mapperMock;
        private readonly MediatorMock _mediatorMock;

        private readonly IMapper _mapper;
        private readonly OrderController _orderController;

        public OrderControllerTests()
        {
            _mapperMock = new();
            _mediatorMock = new();

            _mapper = new Mapper(new MapperConfiguration(configure =>
            configure.AddProfile(new OrderProfile()))) ;

            _orderController = new OrderController(_mediatorMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllOrderAsync_ReturnsReadOrderDTOs()
        {
            //Arrange
            GetAllOrdersQuery query = OrderDataFaker.GetAllOrdersFakedQuery();

            ReadOrderDTO readOrderDTO = OrderDataFaker.GetFakedReadOrderDTO();

            ICollection<ReadOrderDTO> readOrderDTOs = new List<ReadOrderDTO> { readOrderDTO };

            _mediatorMock.MockSendGetAllOrdersQuery(query, readOrderDTOs);

            //Act
            var result = await _orderController.GetAllOrderAsync(
                query.SkipCount, query.SelectionAmount, It.IsAny<CancellationToken>());

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(readOrderDTOs);

            _mediatorMock.Verify();
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnsReadOrderDTO()
        {
            //Arrange
            GetOrderByIdQuery query = OrderDataFaker.GetOrderByIdFakedQuery();

            ReadOrderDTO readOrderDTO = OrderDataFaker.GetFakedReadOrderDTO();

            _mediatorMock.MockSendGetOrderByIdQuery(query, readOrderDTO);

            //Act
            var result = await _orderController.GetOrderAsync(
                query.Id, It.IsAny<CancellationToken>());

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(readOrderDTO);

            _mediatorMock.Verify();
        }

        [Fact]
        public async Task InsertOrderAsync_ReturnsReadOrderDTO()
        {
            //Arrange
            InsertOrderDTO insertOrderDTO = OrderDataFaker.GetFakedInsertOrderDTO();
            var command = _mapper.Map<InsertOrderCommand>(insertOrderDTO);

            ReadOrderDTO readOrderDTO = OrderDataFaker.GetFakedReadOrderDTO(
                insertOrderDTO.BookingId, insertOrderDTO.MenuId);

            _mediatorMock.MockSendInsertOrderCommand(command, readOrderDTO);
            _mapperMock.MockMap(insertOrderDTO, command);

            //Act
            var result = await _orderController.InsertOrderAsync(
                insertOrderDTO, It.IsAny<CancellationToken>());

            var createdAtActionResult = result as CreatedAtActionResult;

            //Assert
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult!.Value.Should().BeEquivalentTo(readOrderDTO);

            _mediatorMock.Verify();
        }

        [Fact]
        public async Task UpdateOrderAsync_ReturnsReadOrderDTO()
        {
            //Arrange
            UpdateOrderDTO updateOrderDTO = OrderDataFaker.GetFakedUpdateOrderDTO();
            var command = _mapper.Map<UpdateOrderCommand>(updateOrderDTO);

            ReadOrderDTO readOrderDTO = OrderDataFaker.GetFakedReadOrderDTO(
                updateOrderDTO.MenuId);

            command.Id = readOrderDTO.Id;

            _mediatorMock.MockSendUpdateOrderCommand(command, readOrderDTO);
            _mapperMock.MockMap(updateOrderDTO, command);

            //Act
            var result = await _orderController.UpdateOrderAsync(readOrderDTO.Id,
                updateOrderDTO, It.IsAny<CancellationToken>());

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(readOrderDTO);

            _mediatorMock.Verify();
        }

        [Fact]
        public async Task DeleteOrderAsync_ReturnsReadOrderDTO()
        {
            //Arrange
            ReadOrderDTO readOrderDTO = OrderDataFaker.GetFakedReadOrderDTO();

            _mediatorMock.MockSendDeleteOrderCommand(readOrderDTO.Id);

            //Act
            var result = await _orderController.DeleteOrderAsync(readOrderDTO.Id,
                It.IsAny<CancellationToken>());

            var okResult = result as NoContentResult;

            //Assert
            okResult.Should().NotBeNull();

            _mediatorMock.Verify();
        }
    }
}
