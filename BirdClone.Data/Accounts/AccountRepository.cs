using BirdClone.Domain;
using BirdClone.Domain.Accounts;

namespace BirdClone.Data.Accounts;

public class AccountRepository : IAccountRepository
{
    private readonly string _connectionString;

    public AccountRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
}