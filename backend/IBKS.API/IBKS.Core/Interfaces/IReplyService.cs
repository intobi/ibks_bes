using IBKS.Core.Models;

namespace IBKS.Core.Interfaces;

public interface IReplyService
{
    Task<List<ReplyViewModel>> GetAllAsync(long ticketId, CancellationToken cancellationToken = default);
    Task PostAsync(long ticketId, ReplyPostModel request, CancellationToken cancellationToken = default);
}
