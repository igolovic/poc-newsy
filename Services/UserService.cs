using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Contracts;

using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;

using Mapster;

using Services.Abstractions;

namespace Services
{
    internal sealed class UserService : IUserService
    {
        private readonly IRepositoryManager _repositoryManager;

        public UserService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

        public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var users = await _repositoryManager.UserRepository.GetAllAsync(cancellationToken);

            var usersDto = users.Adapt<IEnumerable<UserDto>>();

            return usersDto;
        }

        public async Task<UserDto> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var users = await _repositoryManager.UserRepository.GetByIdAsync(userId, cancellationToken);

            if (users is null)
            {
                throw new UserNotFoundException(userId);
            }

            var userDto = users.Adapt<UserDto>();

            return userDto;
        }
    }
}