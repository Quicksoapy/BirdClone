using BirdClone.Domain;
using BirdClone.Domain.Messages;

namespace BirdClone.UnitTests;

public class MessageRepositoryTestTrue : IMessageRepository
{
    public int PostMessageHandler(MessageDto messageModel)
    {
        return 1;
    }

    public IEnumerable<MessageDto> GetMessagesHandler()
    {
        var messageDto = new MessageDto(1)
            .WithContent("Content")
            .WithUsername("Username")
            .WithCreatedOn(DateTime.MinValue)
            .WithUserId(1);
        
        var messageDto1 = new MessageDto(2)
            .WithContent("Content2")
            .WithUsername("Username2")
            .WithCreatedOn(DateTime.MinValue)
            .WithUserId(2);

        List<MessageDto> messageDtos = new List<MessageDto>();
        messageDtos.Add(messageDto);
        messageDtos.Add(messageDto1);
        return messageDtos;
    }

    public IEnumerable<MessageDto> GetMessagesOfUserById(int userId)
    {
        var messageDto = new MessageDto(1)
            .WithContent("Content")
            .WithUsername("Username")
            .WithCreatedOn(DateTime.MinValue)
            .WithUserId(1);
        
        var messageDto1 = new MessageDto(2)
            .WithContent("Content2")
            .WithUsername("Username2")
            .WithCreatedOn(DateTime.MinValue)
            .WithUserId(2);

        List<MessageDto> messageDtos = new List<MessageDto>();
        messageDtos.Add(messageDto);
        messageDtos.Add(messageDto1);
        return messageDtos;
    }

    public IEnumerable<RepostDto> GetRepostsOfUserById(int userId)
    {
        var repostDto = new RepostDto(1)
            .WithUsername("Username")
            .WithContentOp("ContentOriginal")
            .WithCreatedOn(DateTime.MinValue)
            .WithUserId(1)
            .WithUsernameOp("UsernameOriginal")
            .WithCreatedOnOp(DateTime.MinValue)
            .WithUserIdOp(5);
        
        var repostDto1 = new RepostDto(2)
            .WithUsername("Username2")
            .WithContentOp("ContentOriginal2")
            .WithCreatedOn(DateTime.MinValue)
            .WithUserId(2)
            .WithUsernameOp("UsernameOriginal2")
            .WithCreatedOnOp(DateTime.MinValue)
            .WithUserIdOp(2);

        List<RepostDto> repostDtos = new List<RepostDto>();
        repostDtos.Add(repostDto);
        repostDtos.Add(repostDto1);

        return repostDtos;
    }

    public bool UserExist(int userId, string username)
    {
        return true;
    }
}