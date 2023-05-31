using BirdClone.Data.Accounts;
using BirdClone.Domain.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirdClone.Pages;

public class Settings : PageModel
{
    private AccountService _accountService;
    [BindProperty] public Account SettingsModel { get; set; }
    
    public void OnGet()
    {
        _accountService = new AccountService(new AccountRepository());
        
        var userId = Convert.ToInt32(Request.Cookies["UserId"]);

        SettingsModel = _accountService.GetAccountDataById(userId);
    }

    public void OnPost()
    {
        var account = new Account(Convert.ToInt32(Request.Cookies["UserId"]))
            .WithPassword(Globals.GetSha512(SettingsModel.Password));

        _accountService.Edit(account);
    }
}