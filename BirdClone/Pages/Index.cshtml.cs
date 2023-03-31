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
        var dbMessages = new DbMessages();
        var dbUser = new DbUser();

        Messages = dbMessages.GetMessagesHandler().Result;
        if (!string.IsNullOrEmpty(Request.Cookies["UserId"]))
        {
            var account = dbUser.GetAccountDataById(Convert.ToInt32(Request.Cookies["UserId"])).Result;
            Response.Cookies.Append("Username", account.Username);
        }
    }

    public void OnPost()
    {
        var dbMessages = new DbMessages();
        
        MessageModel.CreatedOn = DateTime.UtcNow;
        MessageModel.UserId = Convert.ToInt32(Request.Cookies["UserId"]);
        dbMessages.PostMessageHandler(MessageModel);
        
        OnGet();
    }
}
//TODO look at flexbox, css-grid, bootstrap column rows for nicer mosaic style messages
//https://getbootstrap.com/docs/5.3/utilities/spacing/
//https://css-tricks.com/snippets/css/a-guide-to-flexbox/
//https://css-tricks.com/snippets/css/complete-guide-grid/