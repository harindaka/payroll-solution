using System.Globalization;

namespace PayrollSolution.Domain
{
    public sealed class EmployeeName : ValueObjectBase
    {
        private EmployeeName(string firstName, string lastName, string displayName)
        {
            FirstName = firstName;
            LastName = lastName;
            DisplayName = displayName;
        }

        public string FirstName { get; private init; }
        public string LastName { get; private init; }
        public string DisplayName { get; private init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }

        public static DomainValidationErrorOr<EmployeeName> Create(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Contains(' '))
            {
                return DomainValidationError.InvalidFirstName;
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Contains(' '))
            {
                return DomainValidationError.InvalidLastName;
            }

            var displayName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase($"{firstName} {lastName}");

            return new EmployeeName(firstName, lastName, displayName);
        }
    }
}
