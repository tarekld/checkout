using Microsoft.Extensions.DependencyInjection;

using PaymentGateway.Application.Ports;

using PaymentgGateway.Infrasturcture.Payments;

namespace PaymentGateway.Infrastucture.Payments
{
    public static class HostBuilderExtensions
    {
        public static IServiceCollection ConfigurePaymants(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IPaymentsRepository, PaymentRepository>();
            return serviceCollection;
        }
    }
}
