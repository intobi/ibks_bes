using IBKS.DataAccess.Entities.Base;

namespace IBKS.Core.Interfaces;

public interface ICRUDService<TEntity, TEntityDTO>
        where TEntity : Entity, new()
        where TEntityDTO : class, new()
{
    Task<IList<TEntityDTO>> GetListAsync(CancellationToken cancellationToken = default);
    Task<TEntityDTO> GetOneAsync(long identifier, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long identifier, CancellationToken cancellationToken = default);
    Task<TEntityDTO> PostAsync(TEntityDTO entity, CancellationToken cancellationToken = default);
}
