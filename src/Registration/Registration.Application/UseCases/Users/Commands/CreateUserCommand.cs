using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration.Application.Abstraction;
using Registration.Application.DTOs.Users;
using Registration.Application.Security;
using Registration.Domain.Entities.Users;
using Registration.Domain.Exceptions.Users;

namespace Registration.Application.UseCases.Users.Commands
{
    public class CreateUserCommand : CreateUserDTO, IRequest<bool>
    {

    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public CreateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (checkUser != null)
                throw new UserAlreadyExistsException();

            User user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = PasswordHash.ComputeSHA512HashFromString(request.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}