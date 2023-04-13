using Npgsql;

namespace BirdClone.Domain.Messages;

public class MessageService
{
    private readonly IMessageRepository _messageRepository;

    public MessageService(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<int> PostMessage(Message message)
    {
        
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