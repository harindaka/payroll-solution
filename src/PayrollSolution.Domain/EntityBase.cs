namespace PayrollSolution.Domain
{
    public abstract class EntityBase<TIdentifier> : IEquatable<EntityBase<TIdentifier>>
        where TIdentifier : ValueObjectBase
    {
        protected EntityBase() { }

        protected abstract TIdentifier Id { get; }

        public static bool operator ==(EntityBase<TIdentifier>? left, EntityBase<TIdentifier>? right)
        {
            return left is not null && right is not null && left.Equals(right);
        }

        public static bool operator !=(EntityBase<TIdentifier>? left, EntityBase<TIdentifier>? right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            if (obj is not EntityBase<TIdentifier> entity)
            {
                return false;
            }

            return entity.Id.Equals(Id);
        }

        public virtual bool Equals(EntityBase<TIdentifier>? other)
        {
            if (other is null)
            {
                return false;
            }

            if (other.GetType() != GetType())
            {
                return false;
            }

            return other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
