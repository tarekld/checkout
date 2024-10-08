using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using PaymentGateway.Application.Domain.Model.Queries;

namespace PaymentGateway.Application.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IServiceCollection ConfigureAppliation(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(GetPaymentQuery).Assembly));
            return serviceCollection;
        }
    }
}
