using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.DataAccess.DTOs.User;

namespace IdentityService.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<ReadUserDTO> InsertAsync(InsertUserDTO insertUserDTO, CancellationToken cancellationToken);
        Task<ReadUserDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<TokenDTO> GetUserAsync(string login, string password, CancellationToken cancellationToken);
        Task<ICollection<ReadUserDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<ReadUserDTO> UpdateAsync(int id, UpdateUserDTO updateUserDTO, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
