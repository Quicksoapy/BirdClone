using BirdClone.Models;
using BirdClone.postgres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirdClone.Pages;

public class Account : PageModel
{
    [BindProperty] public AccountModel AccountModel { get; set; }
    
    [BindProperty] public AccountModel EditModel { get; set; }
    
    public void OnGet()
    {
        var databaseHandling = new DatabaseHandling();
        var userId = Convert.ToInt32(Request.Cookies["UserId"]);

        AccountModel = databaseHandling.GetAccountDataById(userId).Result;
    }

    public void OnPost()
    {
        var databaseHandling = new DatabaseHandling();
        AccountModel.Id = Convert.ToInt32(Request.Cookies["UserId"]);
        
        databaseHandling.EditAccount(AccountModel, EditModel);
    }
}