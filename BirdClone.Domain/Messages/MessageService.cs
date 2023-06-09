using System.Security.Authentication;

namespace BirdClone.Domain.Messages;

public class MessageService
{
    private readonly IMessageRepository _messageRepository;

    public MessageService(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }
    
    public List<Message> GetMessagesByUserId(int userId)
    {
        IEnumerable<MessageDto> messageDtos = _messageRepository.GetMessagesOfUserById(userId);
        List<Message> messages = new();

        foreach (MessageDto messageDto in messageDtos)
        {
            messages.Add(new Message(messageDto.Id)
                .WithUserId(messageDto.UserId)
                .WithUsername(messageDto.Username)
                .WithContent(messageDto.Content)
                .WithCreatedOn(messageDto.CreatedOn));
        }

        return messages;
    }

    public List<Repost> GetRepostsByUserId(int userId)
    {
        IEnumerable<RepostDto> repostDtos = _messageRepository.GetRepostsOfUserById(userId);
        
        List<Repost> reposts = new();

        foreach (RepostDto repostDto in repostDtos)
        {
            reposts.Add(new Repost(repostDto.Id)
                .WithUsernameOp(repostDto.UsernameOp)
                .WithUserIdOp(repostDto.UserIdOp)
                .WithContent(repostDto.ContentOp)
                .WithUserId(repostDto.UserId)
                .WithUsername(repostDto.Username)
                .WithCreatedOn(repostDto.CreatedOn));
        }

        return reposts;
    }

    public int PostMessage(Message message)
    {
        var messageDto = new MessageDto(message.Id)
            .WithUserId(message.UserId)
            .WithUsername(message.Username)
            .WithContent(message.Content)
            .WithCreatedOn(message.CreatedOn);

        if (_messageRepository.UserExist(message.UserId, message.Username) == false)
        {
            throw new InvalidCredentialException("User with that id and username not found.");
        }

        if (message.Content.Length > 500)
        {
            throw new ArgumentException("Message content is too long.");
        }
        
        var response = _messageRepository.PostMessageHandler(messageDto);
        return response;
    }
    
    public IEnumerable<Message> GetAllMessages()
    {
        IEnumerable<MessageDto> messageDtos = _messageRepository.GetMessagesHandler();
        List<Message> messages = new();

        foreach (MessageDto messageDto in messageDtos)
        {
            messages.Add(new Message(messageDto.Id)
                .WithUserId(messageDto.UserId)
                .WithUsername(messageDto.Username)
                .WithContent(messageDto.Content)
                .WithCreatedOn(messageDto.CreatedOn));
        }

        return messages;
    }
}