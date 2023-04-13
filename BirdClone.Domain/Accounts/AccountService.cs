using Npgsql;

namespace BirdClone.Domain.Accounts;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public int Login(string username, string password)
    {
        return _accountRepository.LoginHandler(username, password).Result;
    }

    public Account GetAccountDataById(int id)
    {
        var accountDto = _accountRepository.GetAccountDataById(id);
        
        Account account = new()
        {
            Id = accountDto.Result.Id,
            Username = accountDto.Result.Username,
            Bio = accountDto.Result.Bio,
            Country = accountDto.Result.Country,
            CreatedOn = accountDto.Result.CreatedOn,
            Email = accountDto.Result.Email,
            LastLogin = accountDto.Result.LastLogin,
            ProfilePicture = accountDto.Result.ProfilePicture
        };
        return account;
    }
}