using IdentityService.BusinessLogic.DTOs.UserRole;

namespace IdentityService.BusinessLogic.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<ReadUserRoleDTO> GetByIdAsync(int id);
        Task<ICollection<ReadUserRoleDTO>> GetAllAsync();
    }
}
