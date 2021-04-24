using System.Net.Http;
using System.Threading.Tasks;
using AidanTwomey.PaymentsGateway.API.Payments;
using NSubstitute;
using Refit;
using Shouldly;
using Xunit;

namespace AidanTwomey.PaymentGateway.UnitTests
{
    public class GivenAPaymentGreaterThanTheBanLimit
    {
        [Fact]
        public async Task When_MakePayment_Then_ResponseIsUnsuccessful()
        {
            var (request, _) = PaymentInitialiser.InitialiseRequest("4485920504392908", 5, 2024, 199.99m);

            IBank bank = Substitute.For<IBank>();

            bank
                .When(b => b.CreatePayment(Arg.Is<Payment>(p => p.Amount > 100m)))
                .Do(b =>  { throw ApiException.Create(Substitute.For<HttpRequestMessage>(), HttpMethod.Post, Substitute.For<HttpResponseMessage>(), Substitute.For<RefitSettings>()).Result;} );
                
            var service = new PaymentService(bank);

            var response = await service.MakePayment(new Payment{Amount = 199.99m}, request.Card);

            response.Rejected.ShouldBeTrue();
        }
    }
}
