using System.Security.Cryptography;
using System.Text;
using BirdClone.Models;
using BirdClone.postgres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirdClone.Pages;

public class Register : PageModel
{
    [BindProperty] public RegisterModel RegisterModel { get; set; }

    public bool IsLoggedIn { get; set; }

    public void OnPost()
    {
        var dbUser = new DbUser();
        
        RegisterModel.Password = Globals.GetSha512(RegisterModel.Password);
        dbUser.RegisterHandler(RegisterModel);

        Redirect("/");
    }
    public void OnGet()
    {
        
    }
}