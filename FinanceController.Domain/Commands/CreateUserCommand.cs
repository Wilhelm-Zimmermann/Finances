using FinanceController.Domain.Commands.Contracts;
using FinanceController.Domain.Entities;

namespace FinanceController.Domain.Commands
{
    public class CreateUserCommand : ICommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public CreateUserCommand(string name, string email, string password, string role)
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}
