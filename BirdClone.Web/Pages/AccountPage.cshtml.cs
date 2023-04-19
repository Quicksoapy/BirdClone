using BirdClone.Data.Accounts;
using BirdClone.Data.Messages;
using BirdClone.Domain.Accounts;
using BirdClone.Domain.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirdClone.Pages;

public class AccountPage : PageModel
{
    private MessageService _messageService;
    private AccountService _accountService;
    [BindProperty] public Account AccountModel { get; set; }
    [BindProperty] public List<Message> MessagesByUser { get; set; }
    public void OnGet(int id)
    {
        _accountService = new AccountService(new AccountRepository());
        _messageService = new MessageService(new MessageRepository());
        
        AccountModel = _accountService.GetAccountDataById(id).Result;
        MessagesByUser = _messageService.GetMessagesByUserId(id).Result;
    }
}