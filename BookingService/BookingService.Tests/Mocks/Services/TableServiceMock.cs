using BookingService.Application.DTOs.Table;
using BookingService.Application.Interfaces.Services;
using BookingService.Tests.Mocks.Services.Base;
using Moq;

namespace BookingService.Tests.Mocks.Services
{
    public class TableServiceMock : BaseServiceMock<ITableService>
    {
        public TableServiceMock MockInsertItemAsync(
            InsertTableDTO insertItemDTO, ReadTableDTO readItemDTO)
        {
            Setup(service => service.InsertAsync<ReadTableDTO>(
                insertItemDTO, It.IsAny<CancellationToken>()))
            .ReturnsAsync(readItemDTO)
            .Verifiable();

            return this;
        }
    }
}
