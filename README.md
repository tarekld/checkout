The solution of the task is based on clean architecture and CQRS with the help MediatR libaray.
The project PaymentGateway.Application is the Domain centric of the solution, where the Implementation of GetPaymentHandler and PaymentAuthorizationHandler.
GetPaymentHandler retrieves the payment  for the repository
PaymentAuthorizationHandler process the payment authorization by created the payment  and submitting it the bank acquirer
Notice PaymentGateway.Application is defining the ports (interfaces) for the infrastucture aspects, IPaymentsRepository and IAcquirerBankService.
PaymentgGateway.Infrasturcture.Payments implements IPaymentsRepository
PaymentGateway.Infrustructure.AcquiringBank implements IAcquirerBankService
