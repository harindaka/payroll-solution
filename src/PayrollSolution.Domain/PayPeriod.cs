namespace PayrollSolution.Domain
{
    public class PayPeriod : ValueObjectBase
    {
        public const int MinYear = 1900;

        private PayPeriod(int year, Month month, DateOnly startDate, DateOnly endDate)
        {
            Year = year;
            Month = month;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int Year { get; private init; }
        public Month Month { get; private init; }
        public DateOnly StartDate { get; private init; }
        public DateOnly EndDate { get; private init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Year;
            yield return Month;
        }

        public static DomainValidationErrorOr<PayPeriod> Create(int year, Month month)
        {
            if (year < MinYear || year > DateTimeOffset.MaxValue.Year)
            {
                return DomainValidationError.YearOutOfRange;
            }

            var startDateTime = new DateTime(year, (int)month, 1, 0, 0, 0, DateTimeKind.Utc);
            var endDateTime = startDateTime.AddMonths(1).AddDays(-1);

            return new PayPeriod(year, month, new DateOnly(startDateTime.Year, startDateTime.Month, startDateTime.Day),
                new DateOnly(endDateTime.Year, endDateTime.Month, endDateTime.Day));
        }
    }
}
