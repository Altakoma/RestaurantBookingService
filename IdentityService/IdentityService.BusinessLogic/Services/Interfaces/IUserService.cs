using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.DTOs.User;

namespace IdentityService.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<ReadUserDTO> InsertAsync(InsertUserDTO insertUserDTO);
        Task<ReadUserDTO> GetByIdAsync(int id);
        Task<TokenDTO> GetUserAsync(string login, string password);
        Task<ICollection<ReadUserDTO>> GetAllAsync();
        Task<ReadUserDTO> UpdateAsync(int id, UpdateUserDTO updateUserDTO);
        Task DeleteAsync(int id);
    }
}
