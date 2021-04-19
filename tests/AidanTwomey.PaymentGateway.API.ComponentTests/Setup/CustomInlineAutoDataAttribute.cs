using AutoFixture.Xunit2;

namespace AidanTwomey.PaymentGateway.API.ComponentTests
{
    public class CustomInlineAutoDataAttribute : InlineAutoDataAttribute
    {
        public CustomInlineAutoDataAttribute(params object[] values)
            : base(new CustomAutoDataAttribute(), values)
        {
        }
    }
}
