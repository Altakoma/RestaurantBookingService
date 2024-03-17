using Grpc.Core;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.DataAccess.Repositories.Interfaces;

namespace IdentityService.BusinessLogic.Grpc.Servers
{
    public class GrpcServerEmployeeService : EmployeeGrpcService.EmployeeGrpcServiceBase
    {
        private readonly IUserRepository _userRepository;

        public GrpcServerEmployeeService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async override Task<IsUserExistingReply> UserExists(
            IsUserExistingRequest request, ServerCallContext context)
        {
            ReadUserDTO? readUserDTO = await _userRepository
                .GetByIdAsync<ReadUserDTO>(request.UserId, context.CancellationToken);

            bool isUserExisting = readUserDTO is not null;

            var isUserExistingReply = new IsUserExistingReply
            {
                IsUserExisting = isUserExisting,
                UserName = readUserDTO?.Name,
            };

            return isUserExistingReply;
        }
    }
}
