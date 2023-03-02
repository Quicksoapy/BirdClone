using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirdClone.Pages;

public class RegisterModel
{
    public UInt32 Id { get; set; }
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}

public class Register : PageModel
{
    [BindProperty]
    public RegisterModel RegisterModel { get; set; }

    public bool IsLoggedIn { get; set; }

    public void OnPost()
    {
        
        var globals = new Globals();
        var hashedPassword = Globals.GetSha512(RegisterModel.Password);
    }
    public void OnGet()
    {
        
    }
}