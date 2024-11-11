using AutoMapper;
using IBKS.Core.Interfaces;
using IBKS.Core.Models;
using IBKS.DataAccess;
using IBKS.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IBKS.Core.Services;

public class TicketService : CRUDService<Ticket, TicketModel>, ITicketService
{
    public TicketService(IMapper mapper,
        ApplicationContext context,
        IHttpContextAccessor httpContextAccessor)
        : base(mapper, context, httpContextAccessor)
    { }

    public async Task<List<TicketListModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var tickets = await base.GetListAsync(cancellationToken);
        if (tickets is null)
            return new();
        return _mapper.Map<List<TicketListModel>>(tickets);
    }

    public new async Task<TicketViewModel> GetOneAsync(long id, CancellationToken cancellationToken = default)
    {
        var ticket = await base.GetOneAsync(id, cancellationToken);
        if (ticket is null)
            throw new InvalidOperationException("Ticket not found");
        return _mapper.Map<TicketViewModel>(ticket);
    }

    public async Task PostAsync(TicketPostModel request, CancellationToken cancellationToken = default)
    {
        var ticket = _mapper.Map<Ticket>(request);
        await _context.Tickets.AddAsync(ticket, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task PutAsync(long id, TicketViewModel request, CancellationToken cancellationToken = default)
    {
        var ticket = await _context.Tickets
            .Where(x => x.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if(ticket is null)
            throw new InvalidOperationException("Ticket not found");
        Ticket ticketFromRequest = _mapper.Map<Ticket>(request);

        ticket.Title = ticketFromRequest.Title;
        ticket.Description = ticketFromRequest.Description;
        ticket.ApplicationName = ticketFromRequest.ApplicationName;
        ticket.StatusId = ticketFromRequest.StatusId;
        ticket.TicketTypeId = ticketFromRequest.TicketTypeId;
        ticket.LastModified = DateTime.UtcNow;

        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
