using System;
using AidanTwomey.PaymentGateway.API.Model;
using AidanTwomey.PaymentsGateway.API.Validation;
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
            var (request, validator) = InitialiseRequest(number, month, year);

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
            var (request, validator) = InitialiseRequest(number, month, year);

            validator.Validate(request, asOf).ShouldBeOfType<InvalidPayment>();
        }

        private static (MakePaymentRequest, PaymentValidator) InitialiseRequest(string cardNumber, int month, int year) =>
            (new MakePaymentRequest() { Card = new(cardNumber, month, year) }, new PaymentValidator());
        
    }
}