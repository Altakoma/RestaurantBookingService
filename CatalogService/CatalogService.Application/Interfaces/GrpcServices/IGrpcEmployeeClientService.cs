namespace CatalogService.Application.Interfaces.GrpcServices
{
    public interface IGrpcEmployeeClientService
    {
        Task<IsUserExistingReply> IsUserExisting(
            IsUserExistingRequest request, CancellationToken cancellationToken);
    }
}
