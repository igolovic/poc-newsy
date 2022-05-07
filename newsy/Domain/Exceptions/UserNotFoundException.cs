using System;

namespace Domain.Exceptions
{
    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(Guid ownerId)
            : base($"The owner with the identifier {ownerId} was not found.")
        {
        }
    }
}
