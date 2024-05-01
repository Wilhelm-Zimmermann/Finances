namespace FinanceController.Domain.Entities;
public class User : Base
{
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public string? UserIdentityId { get; private set; }

    public IList<Bill> Bills { get; private set; } = new List<Bill>();
    public IList<Privilege> Privileges { get; private set; } = new List<Privilege>();

    public User(string? name, string? email, string? userIdentityId)
    {
        Name = name;
        Email = email;
        UserIdentityId = userIdentityId;
    }
}
