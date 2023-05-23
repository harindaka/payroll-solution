namespace PayrollSolution.Domain
{
    public abstract class ValueObjectBase : IEquatable<ValueObjectBase>
    {
        protected ValueObjectBase() { }

        protected static bool EqualOperator(ValueObjectBase left, ValueObjectBase right)
        {
            if (left is null || right is null)
            {
                return false;
            }
            else
            {
                return ReferenceEquals(left, right) || left.Equals(right);
            }
        }

        protected static bool NotEqualOperator(ValueObjectBase left, ValueObjectBase right)
        {
            return !EqualOperator(left, right);
        }

        public static bool operator ==(ValueObjectBase one, ValueObjectBase two)
        {
            return EqualOperator(one, two);
        }

        public static bool operator !=(ValueObjectBase one, ValueObjectBase two)
        {
            return NotEqualOperator(one, two);
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObjectBase)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public bool Equals(ValueObjectBase? other)
        {
            if (other is null || other.GetType() != GetType())
            {
                return false;
            }

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }
    }
}
