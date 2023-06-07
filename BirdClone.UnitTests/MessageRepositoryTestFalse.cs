using BirdClone.Domain;
using BirdClone.Domain.Messages;

namespace BirdClone.UnitTests;

public class MessageRepositoryTestFalse : IMessageRepository
{
    public int PostMessageHandler(MessageDto messageModel)
    {
        return 0;
    }

    public IEnumerable<MessageDto> GetMessagesHandler()
    {
        return null;
    }

    public IEnumerable<MessageDto> GetMessagesOfUserById(int userId)
    {
        return null;
    }

    public IEnumerable<RepostDto> GetRepostsOfUserById(int userId)
    {
        return null;
    }

    public bool UserExist(int userId, string username)
    {
        return false;
    }
}