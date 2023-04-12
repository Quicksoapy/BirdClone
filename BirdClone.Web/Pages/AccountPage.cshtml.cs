using BirdClone.Domain.Accounts;
using BirdClone.Domain.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirdClone.Pages;

public class AccountPage : PageModel
{
    [BindProperty] public Account AccountModel { get; set; }
    [BindProperty] public List<Message> MessagesByUser { get; set; }
    public void OnGet(int id)
    {
        AccountModel = DbUser.GetAccountDataById(id).Result;
        MessagesByUser = DbMessages.GetMessagesOfUserById(id).Result;
    }
}