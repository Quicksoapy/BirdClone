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
        //TODO check if account exists
        
        var accountDto = new AccountDto(account.Id)
            .WithUsername(account.Username)
            .WithEmail(account.Email)
            .WithPassword(account.Password)
            .WithBio(account.Bio)
            .WithCountry(account.Country)
            .WithProfilePicture(account.ProfilePicture);
        
        _accountRepository.EditAccount(accountDto);
    }
    
    public void Register(Account account)
    {
        var accountDto = new AccountDto(account.Id)
            .WithUsername(account.Username)
            .WithEmail(account.Email)
            .WithPassword(account.Password)
            .WithCreatedOn(account.CreatedOn)
            .WithLastLogin(account.LastLogin);
        
        _accountRepository.RegisterHandler(accountDto);
    }
    public int Login(string username, string password)
    {
        //TODO fix async garbage
        return _accountRepository.LoginHandler(username, password).Result;
    }

    public async Task<Account> GetAccountDataById(int id)
    {
        //TODO fix async garbage
        var accountDto = await _accountRepository.GetAccountDataById(id);
        
        var account = new Account(accountDto.Id)
            .WithUsername(accountDto.Username)
            .WithBio(accountDto.Bio)
            .WithCountry(accountDto.Country)
            .WithCreatedOn(accountDto.CreatedOn)
            .WithEmail(accountDto.Email)
            .WithLastLogin(accountDto.LastLogin)
            .WithProfilePicture(accountDto.ProfilePicture);
        
        return account;
    }
}