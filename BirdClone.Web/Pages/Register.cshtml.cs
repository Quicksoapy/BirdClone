using BirdClone.Data.Accounts;
using BirdClone.Domain.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirdClone.Pages;

public class Register : PageModel
{
    private AccountService _accountService;
    [BindProperty] public Account RegisterModel { get; set; }

    public bool IsLoggedIn { get; set; }

    public void OnPost()
    {
        _accountService = new AccountService(new AccountRepository());

        RegisterModel.WithPassword(Globals.GetSha512(RegisterModel.Password));
        _accountService.Register(RegisterModel);

        Redirect("/");
    }
    public void OnGet()
    {
        
    }
}