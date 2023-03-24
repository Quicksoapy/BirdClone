using BirdClone.Models;
using BirdClone.postgres;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirdClone.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    
    [BindProperty] public MessageModel MessageModel { get; set; }
    
    [BindProperty] public List<MessageModel> Messages { get; set; }

    public void OnGet()
    {
        var databaseHandling = new DatabaseHandling();

        Messages = databaseHandling.GetMessagesHandler().Result;
        
        if (!string.IsNullOrEmpty(Request.Cookies["UserId"]))
        {
            var account = databaseHandling.GetAccountDataById(Convert.ToInt32(Request.Cookies["UserId"])).Result;
            Response.Cookies.Append("Username", account.Username);
        }
    }

    public void OnPost()
    {
        var databaseHandling = new DatabaseHandling();

        MessageModel.CreatedOn = DateTime.UtcNow;
        MessageModel.UserId = Convert.ToInt32(Request.Cookies["UserId"]);
        databaseHandling.PostMessageHandler(MessageModel);
        
        OnGet();
    }
}