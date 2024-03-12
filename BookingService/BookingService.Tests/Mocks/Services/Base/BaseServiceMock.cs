using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Services.Base;
using Moq;

namespace BookingService.Tests.Mocks.Services.Base
{
    public class BaseServiceMock<TService> : Mock<TService> where TService : class, IBaseService
    {
        public BaseServiceMock<TService> MockGetAllAsync<TReadDTO>(ICollection<TReadDTO> readItemDTOs)
        {
            Setup(service => service.GetAllAsync<TReadDTO>(It.IsAny<CancellationToken>()))
            .ReturnsAsync(readItemDTOs)
            .Verifiable();

            return this;
        }

        public BaseServiceMock<TService> MockGetItemAsync<TReadDTO>(TReadDTO readItemDTO)
            where TReadDTO : BaseEntity
        {
            Setup(service => service.GetByIdAsync<TReadDTO>(readItemDTO.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(readItemDTO)
            .Verifiable();

            return this;
        }

        public BaseServiceMock<TService> MockInsertItemAsync<TInsertDTO, TReadDTO>(
            TInsertDTO insertItemDTO, TReadDTO readItemDTO)
        {
            Setup(service => service.InsertAsync<TInsertDTO, TReadDTO>(
                insertItemDTO, It.IsAny<CancellationToken>()))
            .ReturnsAsync(readItemDTO)
            .Verifiable();

            return this;
        }

        public BaseServiceMock<TService> MockDeleteItemAsync(int id)
        {
            Setup(service => service.DeleteAsync(id, It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }
    }
}
