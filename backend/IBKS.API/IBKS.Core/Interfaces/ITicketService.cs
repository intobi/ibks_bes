using IBKS.Core.Models;

namespace IBKS.Core.Interfaces;

public interface ITicketService
{
    Task<List<TicketListModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TicketViewModel> GetOneAsync(long id, CancellationToken cancellationToken = default);
    Task PutAsync(long id, TicketViewModel request, CancellationToken cancellationToken = default);
    Task PostAsync(TicketPostModel request, CancellationToken cancellationToken = default);
}
