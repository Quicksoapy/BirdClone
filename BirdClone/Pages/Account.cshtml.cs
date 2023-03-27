using BirdClone.Models;
using BirdClone.postgres;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BirdClone.Pages;

public class Account : PageModel
{
    [BindProperty] public AccountModel AccountModel { get; set; }
    [BindProperty] public List<MessageModel> MessagesByUser { get; set; }
    public void OnGet(int id)
    {
        var databaseHandling = new DatabaseHandling();
        AccountModel = databaseHandling.GetAccountDataById(id).Result;
        MessagesByUser = databaseHandling.GetMessagesOfUserById(id).Result;
    }
}