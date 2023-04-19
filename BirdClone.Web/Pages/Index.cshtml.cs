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
    
    [BindProperty] public Message MessageModel { get; set; }
    
    [BindProperty] public IEnumerable<Message> Messages { get; set; }

    public void OnGet()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        var connection = new NpgsqlConnection(configuration.GetConnectionString("SqlServer"));
        _messageService = new MessageService(new MessageRepository(connection.ToString()));
        _accountService = new AccountService(new AccountRepository(connection.ToString()));
        Messages = _messageService.GetAllMessages();
        
        if (string.IsNullOrEmpty(Request.Cookies["UserId"])) return;
        var account = _accountService.GetAccountDataById(Convert.ToInt32(Request.Cookies["UserId"])).Result;
        Response.Cookies.Append("Username", account.Username);
    }

    public void OnPost(IConfiguration configuration)
    {
        var connection = new NpgsqlConnection(configuration.GetConnectionString("SqlServer"));
        _messageService = new MessageService(new MessageRepository(connection.ToString()));

        MessageModel.CreatedOn = DateTime.UtcNow;
        MessageModel.UserId = Convert.ToInt32(Request.Cookies["UserId"]);
        _messageService.PostMessage(MessageModel);
        
        OnGet();
    }
}
//TODO look at flexbox, css-grid, bootstrap column rows for nicer mosaic style messages
//https://getbootstrap.com/docs/5.3/utilities/spacing/
//https://css-tricks.com/snippets/css/a-guide-to-flexbox/
//https://css-tricks.com/snippets/css/complete-guide-grid/