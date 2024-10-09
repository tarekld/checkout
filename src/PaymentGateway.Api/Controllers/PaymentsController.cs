using MediatR;

using Microsoft.AspNetCore.Mvc;

using PaymentGateway.Api.Models;
using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Models.Responses;
using PaymentGateway.Application.Domain.Model.Commands;
using PaymentGateway.Application.Domain.Model.Queries;

namespace PaymentGateway.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : Controller
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PostPaymentResponse?>> GetPaymentAsync(Guid id, CancellationToken cancellationToken)
    {
        var payment = await _mediator.Send(new GetPaymentQuery() { Id = id }, cancellationToken);
        return payment == null ? new NotFoundResult() : new OkObjectResult(BuildPaymentResponse(payment));
    }


    [HttpPost]
    public async Task<ActionResult<PostPaymentResponse?>> PostPaymentAsync(PostPaymentRequest postPaymentRequest, CancellationToken cancellationToken)
    {
        var payment = await _mediator.Send(
            new AuthorisePaymentCommand()
            {
                Amount = postPaymentRequest.Amount,
                CardNumber = postPaymentRequest.CardNumber,
                Currency = postPaymentRequest.Currency,
                Cvv = postPaymentRequest.Cvv,
                ExpiryMonth = postPaymentRequest.ExpiryMonth,
                ExpiryYear = postPaymentRequest.ExpiryYear,
                Id = Guid.NewGuid()
            },
            cancellationToken);

        var paymentResponse = BuildPaymentResponse(payment);


        return new OkObjectResult(paymentResponse);
    }

    private PostPaymentResponse BuildPaymentResponse(Application.Domain.Model.Entities.Payment payment)
    {
        return new PostPaymentResponse()
        {
            Id = payment.Id,
            Amount = payment.Amount.Value,
            CardNumberLastFour = Convert.ToInt32(payment.Card.Number.Substring(payment.Card.Number.Length - 4)),
            Currency = payment.Amount.Currency,
            ExpiryYear = payment.Card.ExpiryYear,
            ExpiryMonth = payment.Card.ExpiryMonth,
            Status = GetPaymentStatus(payment.Status)
        };
    }

    private PaymentStatus GetPaymentStatus(Application.Domain.Model.Entities.PaymentStatus status)
    {
        switch (status)
        {
            case Application.Domain.Model.Entities.PaymentStatus.Authorized:
                return PaymentStatus.Authorized;
            case Application.Domain.Model.Entities.PaymentStatus.Declined:
                return PaymentStatus.Declined;
            default:
                return PaymentStatus.Rejected;
        }
    }
}