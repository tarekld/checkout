The solution of the task is based on the Clean Architecture and CQRS with the help of MediatR libaray.
The project PaymentGateway.Application is the Domain centric of the solution, where the Implementation of GetPaymentHandler and PaymentAuthorizationHandler.
GetPaymentHandler retrieves the payment  for the repository and return it through the Api to the Merchant
PaymentAuthorizationHandler received payments from the merchant through API and process  the payment authorization by creating  the payment  and submitting it the bank acquirer
	Notice PaymentGateway.Application is defining the ports (interfaces) for the infrastucture aspects, IPaymentsRepository and IAcquirerBankService.
PaymentgGateway.Infrasturcture.Payments implements IPaymentsRepository
 	PaymentsRepository reads or save the payments from in Memory list storage
  
PaymentGateway.Infrustructure.AcquiringBank implements IAcquirerBankService
	AcquirerBankService craate  httpclient to post the payments to the bank simulator
To Test the endpoints:

1: Start the simulator by run docker-compose up from the top directory of the solution
2: In visual studio set PaymentGateway.Api as the startup project
3: Lunch the project, a swagger documentaion are available,  you can use the swagger UI to post and gets payments
        Or you can  execute the below request in postman

        curl -X 'POST' \
  'https://localhost:7092/api/Payments' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "cardNumber": "2222405343248877",
  "expiryMonth": 4,
  "expiryYear": 2025,
  "currency": "GBP",
  "amount": 100,
  "cvv": "123"
}'
