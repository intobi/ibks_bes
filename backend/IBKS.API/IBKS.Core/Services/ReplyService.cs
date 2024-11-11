using AutoMapper;
using IBKS.Core.Interfaces;
using IBKS.Core.Models;
using IBKS.DataAccess;
using IBKS.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IBKS.Core.Services;

public class ReplyService : CRUDService<Reply, ReplyModel>, IReplyService
{
    public ReplyService(IMapper mapper,
        ApplicationContext context,
        IHttpContextAccessor httpContextAccessor)
        : base(mapper, context, httpContextAccessor)
    { }

    public async Task<List<ReplyViewModel>> GetAllAsync(long ticketId, CancellationToken cancellationToken = default)
    {
        var replyModels = await _context.Replies
            .Where(x => x.TicketId == ticketId)
            .AsNoTracking()
            .Select(reply => new ReplyViewModel
            {
                ReplyMessage = reply.ReplyMessage,
                ReplyDate = reply.ReplyDate
            })
            .OrderBy(x => x.ReplyDate)
            .ToListAsync(cancellationToken);
        if (replyModels is null)
            return new();
        return replyModels;
    }

    public async Task PostAsync(long ticketId, ReplyPostModel request, CancellationToken cancellationToken = default)
    {
        var reply = _mapper.Map<Reply>(request);
        reply.TicketId = ticketId;
        await _context.Replies.AddAsync(reply, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
