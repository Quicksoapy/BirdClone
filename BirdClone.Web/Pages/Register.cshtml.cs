using BirdClone.Data.Accounts;
using BirdClone.Domain.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirdClone.Pages;

public class Register : PageModel
{
    private AccountService _accountService;

    [BindProperty] public RegisterModel RegisterModel { get; set; }

    public bool IsLoggedIn { get; set; }

    public void OnPost()
    {
        _accountService = new AccountService(new AccountRepository());
        var account = new Account()
            .WithUsername(RegisterModel.Username)
            .WithPassword(Globals.GetSha512(RegisterModel.Password))
            .WithEmail(RegisterModel.Email)
            .WithCreatedOn(DateTime.UtcNow)
            .WithLastLogin(DateTime.UtcNow);

        _accountService.Register(account);

        Redirect("/");
    }
    public void OnGet()
    {
    }
}