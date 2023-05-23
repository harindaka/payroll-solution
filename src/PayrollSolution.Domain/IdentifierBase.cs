namespace PayrollSolution.Domain
{
    public class IdentifierBase<TIdentifier> : ValueObjectBase
        where TIdentifier : IdentifierBase<TIdentifier>, new()
    {
        protected IdentifierBase() { }

        public Guid Guid { get; private init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Guid;
        }

        public static TIdentifier Create()
        {
            return new TIdentifier()
            {
                Guid = Guid.NewGuid(),
            };
        }

        public static DomainValidationErrorOr<TIdentifier> CreateFromGuid(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                return DomainValidationError.DefaultGuidIdsNotAllowed;
            }

            return new TIdentifier()
            {
                Guid = guid
            };
        }
    }
}
