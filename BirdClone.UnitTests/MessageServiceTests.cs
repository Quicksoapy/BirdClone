using System.Security.Authentication;
using BirdClone.Domain.Messages;

namespace BirdClone.UnitTests;

public class MessageServiceTests
{
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