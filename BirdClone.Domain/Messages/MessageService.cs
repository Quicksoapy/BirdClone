namespace BirdClone.Domain.Messages;

public class MessageService
{
    private readonly IMessageRepository _messageRepository;

    public MessageService(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<List<Message>> GetMessagesByUserId(int userId)
    {
        IEnumerable<MessageDto> messageDtos = await _messageRepository.GetMessagesOfUserById(userId);
        List<Message> messages = new();
        
        foreach (MessageDto messageDto in messageDtos)
        {
            messages.Add(new Message
            {
                Id = messageDto.Id,
                UserId = messageDto.UserId,
                Username = messageDto.Username,
                Content = messageDto.Content,
                CreatedOn = messageDto.CreatedOn
            });
        }

        return messages;
    }
    
    public async Task<int> PostMessage(Message message)
    {
        var messageDto = new MessageDto()
        {
            Content = message.Content,
            CreatedOn = message.CreatedOn,
            Id = message.Id,
            UserId = message.UserId,
            Username = message.Username
        };
        var response = await _messageRepository.PostMessageHandler(messageDto);
        return response;
    }
    
    public IEnumerable<Message> GetAllMessages()
    {
        IEnumerable<MessageDto> messageDtos = _messageRepository.GetMessagesHandler().Result;
        List<Message> messages = new();

        foreach (MessageDto messageDto in messageDtos)
        {
            messages.Add(new Message
            {
                Id = messageDto.Id,
                UserId = messageDto.UserId,
                Username = messageDto.Username,
                Content = messageDto.Content,
                CreatedOn = messageDto.CreatedOn
            });
        }

        return messages;
    }
}