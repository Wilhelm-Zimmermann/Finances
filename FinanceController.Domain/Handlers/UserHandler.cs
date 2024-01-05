using FinanceController.Domain.Commands;
using FinanceController.Domain.Commands.Contracts;
using FinanceController.Domain.Entities;
using FinanceController.Domain.Handlers.Contracts;
using FinanceController.Domain.Repositories.Contracts;
using FinanceController.Domain.Shared.Utils;

namespace FinanceController.Domain.Handlers
{
    public class UserHandler : IHandler<CreateUserCommand>, IHandler<LoginUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ICommandResult> Handle(CreateUserCommand command)
        {
            command.Password = PasswordHash.Hash(command.Password);
            var user = new User(command.Name, command.Email, command.Password, command.Role);

            await _userRepository.CreateUser(user);

            return new GenericCommandResult(true, "User created", user);
        }

        public async Task<ICommandResult> Handle(LoginUserCommand command)
        {
            var userExists = await _userRepository.GetByEmail(command.Email);

            if(userExists == null)
            {
                return new GenericCommandResult(false, "User does not exists", new { });
            }

            var passwordMatch = PasswordHash.PasswordMatch(command.Password, userExists.Password);

            if(!passwordMatch)
            {
                return new GenericCommandResult(false, "User/Password might be wrong", new { });
            }

            return new GenericCommandResult(true, "User logged successfully", userExists);
        }
    }
}
