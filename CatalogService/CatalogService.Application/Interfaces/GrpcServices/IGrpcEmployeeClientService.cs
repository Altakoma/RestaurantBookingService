namespace CatalogService.Application.Interfaces.GrpcServices
{
    public interface IGrpcEmployeeClientService
    {
        Task<IsUserExistingReply> UserExists(
            IsUserExistingRequest request, CancellationToken cancellationToken);
    }
}
