using FinanceController.Domain.Commands.Contracts;

namespace FinanceController.Domain.Commands
{
    public class LoginUserCommand : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
