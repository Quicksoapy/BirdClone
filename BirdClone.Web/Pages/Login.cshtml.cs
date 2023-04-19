using BirdClone.Domain.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirdClone.Pages;

public class LoginModel
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public int UserId { get; set; } = 0;
}

public class Login : PageModel
{
    private AccountService _accountService;
    [BindProperty] public LoginModel LoginModel { get; set; }

    public bool LoginSuccess { get; set; } = true;
    public void OnGet()
    {
        
    }

    public IActionResult? OnPost(string username, string password)
    {
        var hashedPassword = Globals.GetSha512(LoginModel.Password);
        LoginModel.UserId = _accountService.Login(LoginModel.Username, hashedPassword);
        
        if (LoginModel.UserId == 0)
        {
            LoginSuccess = false;
            return null;
        }
        
        Response.Cookies.Append("UserId", LoginModel.UserId.ToString());
        Response.Cookies.Append("Username", LoginModel.Username);
        Console.WriteLine(Request.Cookies["UserId"]);
        return Redirect("/Index");
    }
}