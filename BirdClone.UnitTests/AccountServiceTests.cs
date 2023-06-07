using BirdClone.Domain.Accounts;
using BirdClone.Domain.Messages;

namespace BirdClone.UnitTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        var accountServiceFalse = new AccountService(new AccountRepositoryTestFalse());
        var accountServiceTrue = new AccountService(new AccountRepositoryTestTrue());
        var messageServiceFalse = new MessageService(new MessageRepositoryTestFalse());
        var messageServiceTrue = new MessageService(new MessageRepositoryTestTrue());
    }

    [Test]
    public void GetAccountDataById_Creates_Account_From_AccountDto_Parameters()
    {
        var accountServiceFalse = new AccountService(new AccountRepositoryTestFalse());
        var accountServiceTrue = new AccountService(new AccountRepositoryTestTrue());

        var accountFalse = accountServiceFalse.GetAccountDataById(1);
        
        
        Assert.Pass();
    }
}