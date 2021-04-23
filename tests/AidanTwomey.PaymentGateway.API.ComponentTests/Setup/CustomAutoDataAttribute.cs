using AutoFixture;
using AutoFixture.Xunit2;

namespace AidanTwomey.PaymentGateway.API.ComponentTests
{
    public class CustomAutoDataAttribute : AutoDataAttribute
    {
        public CustomAutoDataAttribute() : base(Customizations)
        {
        }

        public static IFixture Customizations()
        {
            return new Fixture();
        }
    }
}
