using IdentityService.BusinessLogic.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> InsertAsync(InsertUserDTO item);
        Task<ReadUserDTO?> GetByIdAsync(int id);
        Task<ICollection<ReadUserDTO>?> GetAllAsync();
        Task<string> UpdateAsync(UpdateUserDTO item);
        Task<string> DeleteAsync(int id);
    }
}
