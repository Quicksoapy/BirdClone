namespace BirdClone.Domain.Messages;

public class MessageService
{
    private readonly IMessageRepository _messageRepository;

    public MessageService(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }
    //TODO unit test these, utilize irepo that always returns true and false
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
            return 0;
        }

        if (message.Content.Length > 500)
        {
            return 0;
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