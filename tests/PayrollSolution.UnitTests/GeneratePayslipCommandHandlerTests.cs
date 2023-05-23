using PayrollSolution.Application.PayslipGeneration;
using PayrollSolution.Domain;

namespace PayrollSolution.UnitTests
{
    public class GeneratePayslipCommandHandlerTests
    {
        [Fact]
        public async void WhenRequestIsValid_Then_PayslipIsGeneratedSuccessfully()
        {
            //Arrange
            var command = new GeneratePayslipCommand
            {
                FirstName = "John",
                LastName = "Doe",
                AnnualSalary = 60050,
                PayPeriodMonth = Application.Common.Models.Month.March,
                PayPeriodYear = 2023,
                SuperRate = 0.09m
            };

            var expectedName = "John Doe";
            var expectedPayPeriodStartDate = new DateOnly(2023, 03, 01);
            var expectedPayPeriodEndDate = new DateOnly(2023, 03, 31);
            var expectedGrossIncome = 5004.17m;
            var expectedIncomeTax = 919.58m;
            var expectedNetIncome = 4084.59m;
            var expectedSuper = 450.38m;

            Domain.IncomeTaxRangeSpecification incomeTaxRangeSpec = CreateIncomeTaxRangeSpec();

            var incomeTaxRangeSpecRepo = new Mock<IIncomeTaxRangeSpecificationRepository>();
            incomeTaxRangeSpecRepo.Setup(m => m.RetrieveActive(It.IsAny<CancellationToken>())).ReturnsAsync(incomeTaxRangeSpec);

            var handler = new GeneratePayslipCommandHandler(incomeTaxRangeSpecRepo.Object);
            var cancellationToken = new CancellationToken();

            //Act
            var response = await handler.Handle(command, cancellationToken);

            //Assert

            //Assert the repo is being used to retrieve active tax range spec
            incomeTaxRangeSpecRepo.Verify(m => m.RetrieveActive(It.Is<CancellationToken>(p => p == cancellationToken)), Times.Once);

            response.Should().NotBeNull();
            response.IsValid.Should().BeTrue();

            var payslip = response.Result;
            payslip.Name.Should().Be(expectedName);
            payslip.PayPeriodStartDate.Should().Be(expectedPayPeriodStartDate);
            payslip.PayPeriodEndDate.Should().Be(expectedPayPeriodEndDate);
            payslip.GrossIncome.Should().Be(expectedGrossIncome);
            payslip.IncomeTax.Should().Be(expectedIncomeTax);
            payslip.NetIncome.Should().Be(expectedNetIncome);
            payslip.Super.Should().Be(expectedSuper);
        }

        private static IncomeTaxRangeSpecification CreateIncomeTaxRangeSpec()
        {
            var incomeTaxRangeSpecId = IncomeTaxRangeSpecificationId.Create();

            var initialSlab = IncomeTaxRange.Create(14_000m, 0.105m);
            var secondSlab = IncomeTaxRange.Create(48_000m, 0.175m);
            var thirdSlab = IncomeTaxRange.Create(70_000m, 0.3m);
            var fourthSlab = IncomeTaxRange.Create(180_000m, 0.33m);
            var lastSlabRate = 0.39m;

            var incomeTaxRangeSpec = IncomeTaxRangeSpecification.CreateFromRanges(incomeTaxRangeSpecId, new List<IncomeTaxRange>
            {
                initialSlab.Result, secondSlab.Result, thirdSlab.Result, fourthSlab.Result
            }, lastSlabRate);

            return incomeTaxRangeSpec.Result;
        }
    }    
}
