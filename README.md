The solution of the task is based on the Clean Architecture and CQRS with the help of MediatR libaray.
The project PaymentGateway.Application is the Domain centric of the solution, where the Implementation of GetPaymentHandler and PaymentAuthorizationHandler.
GetPaymentHandler retrieves the payment  for the repository and return it through the Api to the Merchant
PaymentAuthorizationHandler received payments from the merchant through API and process  the payment authorization by creating  the payment  and submitting it the bank acquirer
Notice PaymentGateway.Application is defining the ports (interfaces) for the infrastucture aspects, IPaymentsRepository and IAcquirerBankService.
PaymentgGateway.Infrasturcture.Payments implements IPaymentsRepository
PaymentGateway.Infrustructure.AcquiringBank implements IAcquirerBankService
