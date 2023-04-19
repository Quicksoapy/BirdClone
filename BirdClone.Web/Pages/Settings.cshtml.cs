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
        var userId = Convert.ToInt32(Request.Cookies["UserId"]);

        SettingsModel = _accountService.GetAccountDataById(userId).Result;
    }

    public void OnPost()
    {
        SettingsModel.Id = Convert.ToInt32(Request.Cookies["UserId"]);
        SettingsModel.Password = Globals.GetSha512(SettingsModel.Password);
        
        _accountService.Edit(SettingsModel);
    }
}