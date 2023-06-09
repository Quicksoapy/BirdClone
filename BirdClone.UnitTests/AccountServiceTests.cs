using System.Buffers.Text;
using System.Diagnostics;
using System.Security.Authentication;
using BirdClone.Domain.Accounts;
using BirdClone.Domain.Messages;

namespace BirdClone.UnitTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void GetAccountDataById_Creates_Account_From_AccountDto_Parameters()
    {
        var accountServiceFalse = new AccountService(new AccountRepositoryTestFalse());
        var accountServiceTrue = new AccountService(new AccountRepositoryTestTrue());

        var accountFalse = accountServiceFalse.GetAccountDataById(1);
        var accountTrue = accountServiceTrue.GetAccountDataById(2);

        var account = new Account(2)
            .WithBio("Bio")
            .WithCountry("Country")
            .WithEmail("Email")
            .WithPassword("Password")
            .WithUsername("Username")
            .WithCreatedOn(DateTime.UnixEpoch)
            .WithLastLogin(DateTime.UnixEpoch)
            .WithProfilePicture("UrlToProfilePicture");

        Assert.AreEqual(account, accountTrue);
        Assert.AreEqual(new Account(), accountFalse);
    }

    [Test]
    public void Account_Will_Not_Initialize_Without_Proper_Parameters()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var account = new Account()
                .WithPassword("")
                .WithEmail("")
                .WithUsername("")
                .WithCreatedOn(DateTime.MinValue)
                .WithLastLogin(DateTime.MinValue);
        });
    }
}