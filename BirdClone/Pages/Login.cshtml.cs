using System.Text;
using BirdClone.postgres;
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
    [BindProperty] public LoginModel LoginModel { get; set; }
    public void OnGet()
    {
        
    }

    public void OnPost(string username, string password)
    {
        var databaseHandling = new DatabaseHandling();
        
        var hashedPassword = Globals.GetSha512(LoginModel.Password);
        LoginModel.UserId = databaseHandling.LoginHandler(LoginModel.Username, hashedPassword).Result;
        Response.Cookies.Append("UserId", LoginModel.UserId.ToString());
        Response.Cookies.Append("Username", LoginModel.Username);
        Console.WriteLine(Request.Cookies["UserId"]);
        Redirect("/");
    }
}