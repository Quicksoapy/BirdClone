using BirdClone.Models;
using BirdClone.postgres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirdClone.Pages;

public class Settings : PageModel
{
    [BindProperty] public AccountModel SettingsModel { get; set; }
    
    public void OnGet()
    {
        var userId = Convert.ToInt32(Request.Cookies["UserId"]);

        SettingsModel = DbUser.GetAccountDataById(userId).Result;
    }

    public void OnPost()
    {
        var dbUser = new DbUser();
        SettingsModel.Id = Convert.ToInt32(Request.Cookies["UserId"]);
        SettingsModel.Password = Globals.GetSha512(SettingsModel.Password);
        
        DbUser.EditAccount(SettingsModel);
    }
}