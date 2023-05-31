using System.Xml.Xsl;
using BirdClone.Data.Accounts;
using BirdClone.Data.Messages;
using BirdClone.Domain.Accounts;
using BirdClone.Domain.Messages;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace BirdClone.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private MessageService _messageService;
    private AccountService _accountService;
    
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    
    [BindProperty] public MessageModel MessageModel { get; set; }
    
    [BindProperty] public IEnumerable<Message> Messages { get; set; }

    public void OnGet()
    {
        _messageService = new MessageService(new MessageRepository());
        _accountService = new AccountService(new AccountRepository());

        if (string.IsNullOrEmpty(Request.Cookies["UserId"])) return;
        var account = _accountService.GetAccountDataById(Convert.ToInt32(Request.Cookies["UserId"]));
        Response.Cookies.Append("Username", account.Username);
        
       
    }

    public void OnPost()
    {
        _messageService = new MessageService(new MessageRepository());
        var message = new Message()
            .WithUserId(Convert.ToInt32(Request.Cookies["UserId"]))
            .WithContent(MessageModel.Content)
            .WithUsername(Request.Cookies["Username"] ?? throw new InvalidOperationException())
            .WithCreatedOn(DateTime.UtcNow);

        _messageService.PostMessage(message);

        OnGet();
    }
}
//TODO look at flexbox, css-grid, bootstrap column rows for nicer mosaic style messages
//https://getbootstrap.com/docs/5.3/utilities/spacing/
//https://css-tricks.com/snippets/css/a-guide-to-flexbox/
//https://css-tricks.com/snippets/css/complete-guide-grid/