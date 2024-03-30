﻿using AutoMapper;
using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.BusinessLogic.DTOs.User.Messages;
using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.KafkaMessageBroker.Interfaces.Producers;
using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.BusinessLogic.TokenGenerators;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Exceptions;
using IdentityService.DataAccess.Repositories.Interfaces;

namespace IdentityService.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _tokenGenerator;
        //private readonly IRefreshTokenService _refreshTokenService;
        //private readonly IUserMessageProducer _userMessageProducer;
        private readonly ICookieService _cookieService;

        public UserService(IUserRepository userRepository,
            IMapper mapper, ICookieService cookieService,
            ITokenGenerator tokenGenerator//,
            //IRefreshTokenService refreshTokenService,
            //IUserMessageProducer userMessageProducer
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _cookieService = cookieService;
            _tokenGenerator = tokenGenerator;
            //_refreshTokenService = refreshTokenService;
            //_userMessageProducer = userMessageProducer;
        }

        public async Task DeleteAsync(int id,
            CancellationToken cancellationToken)
        {
            //await _refreshTokenService.DeleteByIdAsync(id.ToString(),
            //                                           cancellationToken);

            await _userRepository.DeleteAsync(id, cancellationToken);

            bool isDeleted = await _userRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            var message = new DeleteUserMessageDTO { Id = id };

            //await _userMessageProducer.ProduceMessageAsync(message,
            //   cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(
                    nameof(DeleteAsync), id.ToString(), typeof(User));
            }
        }

        public async Task<ICollection<ReadUserDTO>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadUserDTO> readUserDTOs =
                await _userRepository.GetAllAsync<ReadUserDTO>(cancellationToken);

            return readUserDTOs;
        }

        public async Task<ReadUserDTO> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            ReadUserDTO? readUserDTO = await _userRepository
                                      .GetByIdAsync<ReadUserDTO>(id, cancellationToken);

            if (readUserDTO is null)
            {
                throw new NotFoundException(id.ToString(), typeof(User));
            }

            return readUserDTO;
        }

        public async Task<AccessTokenDTO> GetUserAsync(string login,
            string password, CancellationToken cancellationToken)
        {
            ReadUserDTO? readUserDTO = await _userRepository
                .GetUserAsync<ReadUserDTO>(login, password, cancellationToken);

            if (readUserDTO is null)
            {
                throw new NotFoundException(login, typeof(User));
            }

            (AccessTokenDTO tokenDTO, string refreshToken) = _tokenGenerator
                .GenerateTokens(readUserDTO.Name, readUserDTO.UserRoleName,
                                readUserDTO.Id.ToString());

            //await _refreshTokenService.SetAsync(readUserDTO.Id.ToString(),
            //    refreshToken, RefreshTokenService.ExpirationTime, cancellationToken);

            SetUsersCookie(refreshToken, readUserDTO);

            return tokenDTO;
        }

        public async Task<ReadUserDTO> InsertAsync(InsertUserDTO item,
            CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(item);

            user = await _userRepository.InsertAsync(user, cancellationToken);

            bool isInserted = await _userRepository
                                    .SaveChangesToDbAsync(cancellationToken);

            if (!isInserted)
            {
                throw new DbOperationException(
                    nameof(InsertAsync), user.Id.ToString(), typeof(User));
            }

            var message = _mapper.Map<InsertUserMessageDTO>(user);

            //await _userMessageProducer.ProduceMessageAsync(message,
            //    cancellationToken);

            ReadUserDTO readUserDTO = (await _userRepository
                .GetByIdAsync<ReadUserDTO>(user.Id, cancellationToken))!;

            return readUserDTO;
        }

        public async Task<ReadUserDTO> UpdateAsync(int id,
            UpdateUserDTO item, CancellationToken cancellationToken)
        {
            //await _refreshTokenService.DeleteByIdAsync(id.ToString(), 
            //    cancellationToken);

            var user = _mapper.Map<User>(item);

            user.Id = id;

            _userRepository.Update(user);

            bool isUpdated = await _userRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isUpdated)
            {
                throw new DbOperationException(
                    nameof(UpdateAsync), user.Id.ToString(), typeof(User));
            }

            var message = _mapper.Map<UpdateUserMessageDTO>(user);

            //await _userMessageProducer.ProduceMessageAsync(message,
            //    cancellationToken);

            ReadUserDTO readUserDTO = (await _userRepository
                .GetByIdAsync<ReadUserDTO>(id, cancellationToken))!;

            return readUserDTO;
        }

        private void SetUsersCookie(string refreshToken, ReadUserDTO readUserDTO)
        {
            _cookieService.SetCookieValue(CookieService.RefreshTokenCookieName,
                                          refreshToken);
            _cookieService.SetCookieValue(CookieService.UserIdCookieName,
                                          readUserDTO.Id.ToString());
        }
    }
}
