using Microsoft.Extensions.DependencyInjection;

using PaymentGateway.Application.Ports;
using PaymentGateway.Infrustructure.AcquiringBank;

namespace PaymentGateway.Infrastucture.AcquiringBank
{
    public static class HostBuilderExtensions
    {
        public static IServiceCollection ConfigureAcquiringBank(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IAcquirerBankService, AcquirerBankService>();
            serviceCollection.AddHttpClient<IAcquirerBankService, AcquirerBankService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:8080");
            });

            return serviceCollection;
        }
    }
}
