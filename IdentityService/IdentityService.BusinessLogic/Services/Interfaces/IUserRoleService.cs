using IdentityService.BusinessLogic.DTOs.UserRole;

namespace IdentityService.BusinessLogic.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<ReadUserRoleDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<ICollection<ReadUserRoleDTO>> GetAllAsync(CancellationToken cancellationToken);
    }
}
