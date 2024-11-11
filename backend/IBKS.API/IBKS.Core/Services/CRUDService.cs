using AutoMapper;
using IBKS.Core.Interfaces;
using IBKS.DataAccess;
using IBKS.DataAccess.Entities.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IBKS.Core.Services;

public class CRUDService<TEntity, TEntityDTO> : ICRUDService<TEntity, TEntityDTO>
     where TEntity : Entity, new()
     where TEntityDTO : class, new()
{
    protected readonly IMapper _mapper;
    protected readonly ApplicationContext _context;
    protected readonly IHttpContextAccessor _httpContextAccessor;

    public CRUDService(IMapper mapper, ApplicationContext context, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _context = context;
        _httpContextAccessor = httpContextAccessor;

    }

    public async virtual Task<bool> DeleteAsync(long identifier, CancellationToken cancellationToken = default)
    {
        try
        {
            var target = await _context.Set<TEntity>().FindAsync(identifier, cancellationToken);
            if (target is null)
                return false;

            _context.Set<TEntity>().Remove(target);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public virtual async Task<IList<TEntityDTO>> GetListAsync(CancellationToken cancellationToken = default)
    {
        var result = await _context.Set<TEntity>().ToListAsync(cancellationToken);
        return _mapper.Map<List<TEntityDTO>>(result.OrderByDescending(e => e.Id));
    }

    public virtual async Task<TEntityDTO> GetOneAsync(long identifier, CancellationToken cancellationToken = default)
    {
        var target = await _context.Set<TEntity>().FindAsync(identifier, cancellationToken);
        return _mapper.Map<TEntityDTO>(target);

    }

    public virtual async Task<TEntityDTO> PostAsync(TEntityDTO entity, CancellationToken cancellationToken = default)
    {
        var _entity = _mapper.Map<TEntity>(entity);
        await _context.Set<TEntity>().AddAsync(_entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        var result = await _context.Set<TEntity>().FindAsync(_entity.Id);
        return _mapper.Map<TEntityDTO>(result);
    }
}
