using System.Reflection;

namespace PayrollSolution.Domain
{
    //Ref: https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types#implement-an-enumeration-base-class
    public abstract class EnumerationBase : IComparable
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        protected EnumerationBase(int id, string name) => (Id, Name) = (id, name);

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : EnumerationBase =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                     .Select(f => f.GetValue(null))
                     .Cast<T>();

        public override bool Equals(object? obj)
        {
            if (obj is not EnumerationBase otherValue)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object? other) => other is null ? 1 : Id.CompareTo(((EnumerationBase)other).Id);

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }
}
