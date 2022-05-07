using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;

namespace Services.Abstractions
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<UserDto> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}