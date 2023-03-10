using System.Text;
using BirdClone.postgres;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirdClone.Pages;

public class LoginModel
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}

public class Login : PageModel
{
    public LoginModel LoginModel { get; set; }
    public void OnGet()
    {
        HttpContext.Session.Set("Uid", Encoding.ASCII.GetBytes("1"));
        HttpContext.Session.CommitAsync();
    }

    public void OnPost(string username, string password)
    {
        var databaseHandling = new DatabaseHandling();
        
        var hashedPassword = Globals.GetSha512(LoginModel.Password);
        databaseHandling.LoginHandler(LoginModel.Username, hashedPassword);

        Redirect("/");
    }
}