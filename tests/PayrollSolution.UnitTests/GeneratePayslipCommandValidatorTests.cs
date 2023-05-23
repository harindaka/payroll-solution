using PayrollSolution.Application.PayslipGeneration;

namespace PayrollSolution.UnitTests
{
    public class GeneratePayslipCommandValidatorTests
    {
        [Fact]
        public async void When_InvalidCommandIsValidated_Then_ValidationFails()
        {
            //Arrange
            var command = new GeneratePayslipCommand
            {
                FirstName = "",
                LastName = "",
                AnnualSalary = -1000,
                PayPeriodMonth = 0,
                PayPeriodYear = 1,
                SuperRate = 2
            };

            var validator = new GeneratePayslipCommandValidator();

            //Act
            var validationResult = await validator.ValidateAsync(command);

            //Assert           
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should()
                .Contain(e => e.PropertyName == nameof(GeneratePayslipCommand.FirstName));
            validationResult.Errors.Should()
                .Contain(e => e.PropertyName == nameof(GeneratePayslipCommand.LastName));
            validationResult.Errors.Should()
                .Contain(e => e.PropertyName == nameof(GeneratePayslipCommand.AnnualSalary));
            validationResult.Errors.Should()
                .Contain(e => e.PropertyName == nameof(GeneratePayslipCommand.PayPeriodMonth));
            validationResult.Errors.Should()
                .Contain(e => e.PropertyName == nameof(GeneratePayslipCommand.PayPeriodYear));
            validationResult.Errors.Should()
                .Contain(e => e.PropertyName == nameof(GeneratePayslipCommand.SuperRate));
        }

        [Fact]
        public async void When_ValidCommandIsValidated_Then_ValidationSucceeds()
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

            var validator = new GeneratePayslipCommandValidator();

            //Act
            var validationResult = await validator.ValidateAsync(command);

            //Assert           
            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should().BeEmpty();
        }
    }
}
