using IdentityService.BusinessLogic.DTOs.TokenDTOs;
using IdentityService.BusinessLogic.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<ReadUserDTO> InsertAsync(InsertUserDTO insertUserDTO);
        Task<ReadUserDTO?> GetByIdAsync(int id);
        Task<TokensDTO> GetUserAsync(string login, string password);
        Task<ICollection<ReadUserDTO>?> GetAllAsync();
        Task<ReadUserDTO> UpdateAsync(int id, UpdateUserDTO updateUserDTO);
        Task DeleteAsync(int id);
    }
}
