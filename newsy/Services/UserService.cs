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
            var owners = await _repositoryManager.UserRepository.GetAllAsync(cancellationToken);

            var ownersDto = owners.Adapt<IEnumerable<UserDto>>();

            return ownersDto;
        }

        public async Task<UserDto> GetByIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
        {
            var owner = await _repositoryManager.UserRepository.GetByIdAsync(ownerId, cancellationToken);

            if (owner is null)
            {
                throw new UserNotFoundException(ownerId);
            }

            var UserDto = owner.Adapt<UserDto>();

            return UserDto;
        }
    }
}