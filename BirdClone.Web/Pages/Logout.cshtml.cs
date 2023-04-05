using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirdClone.Pages;

public class Logout : PageModel
{
    public void OnGet()
    {
        RemoveCookies();
    }

    private IActionResult RemoveCookies()
    {
        foreach (var cookie in HttpContext.Request.Cookies)
        {
            Response.Cookies.Delete(cookie.Key);
        }

        return Redirect("/Index");
    }
}