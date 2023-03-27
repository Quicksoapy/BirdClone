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
        var databaseHandling = new DatabaseHandling();
        var userId = Convert.ToInt32(Request.Cookies["UserId"]);

        SettingsModel = databaseHandling.GetAccountDataById(userId).Result;
    }

    public void OnPost()
    {
        var databaseHandling = new DatabaseHandling();
        SettingsModel.Id = Convert.ToInt32(Request.Cookies["UserId"]);
        SettingsModel.Password = Globals.GetSha512(SettingsModel.Password);
        
        databaseHandling.EditAccount(SettingsModel);
    }
}