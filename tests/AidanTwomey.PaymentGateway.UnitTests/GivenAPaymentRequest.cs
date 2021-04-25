using System;
using System.Threading.Tasks;
using AidanTwomey.PaymentGateway.API.Model;
using AidanTwomey.PaymentsGateway.API.Payments;
using AidanTwomey.PaymentsGateway.API.Validation;
using NSubstitute;
using Shouldly;
using Xunit;

namespace AidanTwomey.PaymentGateway.UnitTests
{
    public class GivenAPaymentRequest
    {
        private const string ValidCardNumber = "4916712854785842";
        private readonly DateTime asOf = new DateTime(2021, 4, 20);

        [Theory]
        [InlineData(ValidCardNumber, 12, 2021, "754")]
        [InlineData(ValidCardNumber, 4, 2021, "754")]
        public void And_Card_Is_Well_Formed_Then_Validation_Returns_ValidPayment(
            string number, int month, int year, string cvv
        )
        {
            var (request, validator) = PaymentInitialiser.InitialiseRequest(number, month, year, 0.99m);

            validator.Validate(request, asOf).ShouldBeOfType<ValidPayment>();
        }

        [Theory]
        [InlineData("123", 12, 2021, "754")]
        [InlineData("491671285478584a", 12, 2021, "754")]
        [InlineData(ValidCardNumber, 12, 2020, "754")]
        [InlineData(ValidCardNumber, 3, 2021, "754")]
        public void And_Card_Is_Badly_Formed_Then_Validation_Returns_InvalidPayment(
            string number, int month, int year, string cvv
        )
        {
            var (request, validator) = PaymentInitialiser.InitialiseRequest(number, month, year, 0.99m);

            validator.Validate(request, asOf).ShouldBeOfType<InvalidPayment>();
        }

        [Fact]
        public void And_Payment_Amount_Is_Zero_Then_Validation_Returns_ValidPayment()
        {
            new PaymentValidator()
                .Validate(new MakePaymentRequest() { Card = new(ValidCardNumber, 11, 2022), Amount = 0m }, asOf)
                .ShouldBeOfType<InvalidPayment>();
        }

        [Fact]
        public async Task And_Payment_Is_Validated_Then_MakePayment_Returns_ValidPaymentAsync()
        {
            Payment payment = new Payment();

            IBank bank = Substitute.For<IBank>();

            bank
                .CreatePayment(payment)
                .Returns(Task.FromResult(new ProcessedPayment(){success = true} ));
            
            var service = new PaymentService(bank);

            var transaction = await service.MakePayment(payment, new Card(ValidCardNumber, 9,2025));

            transaction.Rejected.ShouldBeFalse();
        }
    }
}
