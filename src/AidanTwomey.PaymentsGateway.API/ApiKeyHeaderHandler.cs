using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AidanTwomey.PaymentsGateway.API
{
    class ApiKeyHeaderHandler : DelegatingHandler
    {
        private readonly IConfiguration configuration;

        public ApiKeyHeaderHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("x-api-key", configuration["BankApiKey"]);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
