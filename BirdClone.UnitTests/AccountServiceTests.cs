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

    [Test]
    public void Message_Will_Not_Initialize_Without_Proper_Parameters()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var message = new Message()
                .WithUsername("")
                .WithContent("")
                .WithUserId(0)
                .WithCreatedOn(DateTime.MinValue);
        });
    }

    [Test]
    public void PostMessage_Will_Properly_Create_A_MessageDto()
    {
        var messageServiceFalse = new MessageService(new MessageRepositoryTestFalse());
        var messageServiceTrue = new MessageService(new MessageRepositoryTestTrue());

        Assert.Throws<InvalidCredentialException>(() =>
            messageServiceFalse.PostMessage(new Message()
                .WithContent("Content")
                .WithUsername("Username")
                .WithCreatedOn(DateTime.Now)
                .WithUserId(9)));
        
        Assert.Throws<ArgumentException>(() =>
            messageServiceTrue.PostMessage(new Message()
                .WithContent("ac ut consequat semper viverra nam libero justo laoreet sit amet cursus sit amet dictum sit amet justo donec enim diam vulputate ut pharetra sit amet aliquam id diam maecenas ultricies mi eget mauris pharetra et ultrices neque ornare aenean euismod elementum nisi quis eleifend quam adipiscing vitae proin sagittis nisl rhoncus mattis rhoncus urna neque viverra justo nec ultrices dui sapien eget mi proin sed libero enim sed faucibus turpis in eu mi bibendum neque egestas congue quisque egestas diam in arcu cursus euismod quis viverra nibh cras pulvinar mattis nunc sed blandit libero volutpat sed cras ornare arcu")
                .WithUsername("Username")
                .WithCreatedOn(DateTime.Now)
                .WithUserId(9)));


    }
}