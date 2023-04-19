using Npgsql;

namespace BirdClone.Domain.Accounts;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public void Edit(Account account)
    {
        var accountDto = new AccountDto
        {
            Username = account.Username,
            Email = account.Email,
            Password = account.Password,
            Bio = account.Bio,
            Country = account.Country,
            ProfilePicture = account.ProfilePicture
        };
        _accountRepository.EditAccount(accountDto);
    }
    
    public void Register(Account account)
    {
        var accountDto = new AccountDto
        {
            Username = account.Username,
            Email = account.Email,
            Password = account.Password,
            CreatedOn = DateTime.UtcNow,
            LastLogin = DateTime.UtcNow
        };
        _accountRepository.RegisterHandler(accountDto);
    }
    public int Login(string username, string password)
    {
        return _accountRepository.LoginHandler(username, password).Result;
    }

    public async Task<Account> GetAccountDataById(int id)
    {
        var accountDto = await _accountRepository.GetAccountDataById(id);
        
        Account account = new()
        {
            Id = accountDto.Id,
            Username = accountDto.Username,
            Bio = accountDto.Bio,
            Country = accountDto.Country,
            CreatedOn = accountDto.CreatedOn,
            Email = accountDto.Email,
            LastLogin = accountDto.LastLogin,
            ProfilePicture = accountDto.ProfilePicture
        };
        return account;
    }
}