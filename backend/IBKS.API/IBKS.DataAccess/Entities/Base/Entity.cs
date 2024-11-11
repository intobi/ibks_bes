namespace IBKS.DataAccess.Entities.Base;

public class Entity : IEquatable<Entity>, IEqualityComparer<Entity>
{
    public virtual long Id { get; set; }

    public bool Equals(Entity other)
    {
        return Id == other.Id;
    }

    public bool Equals(Entity x, Entity y)
    {
        if (x is null || y is null)
            return false;

        return x.Id == y.Id;
    }

    public int GetHashCode(Entity obj)
    {
        if (obj is null) return 0;

        unchecked
        {
            return obj.Id.GetHashCode();
        }
    }
}
