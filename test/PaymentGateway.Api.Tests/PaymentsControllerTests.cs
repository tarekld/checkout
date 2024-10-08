using System.Net;
using System.Net.Http.Json;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using PaymentGateway.Api.Controllers;
using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Models.Responses;
using PaymentGateway.Application.Domain.Model.Entities;
using PaymentGateway.Application.Ports;

using PaymentgGateway.Infrasturcture.Payments;

using PaymentStatus = PaymentGateway.Api.Models.PaymentStatus;

namespace PaymentGateway.Api.Tests;

public class PaymentsControllerTests
{
    private readonly Random _random = new();

    [Fact]
    public async Task RetrievesAPaymentSuccessfully()
    {
        // Arrange
        var payment = new Payment("1234567894758614", 12, 2024, "", "GBP", 1);


        var paymentsRepository = new PaymentRepository();
        HttpClient httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("localhost:8080");
        paymentsRepository.Add(payment);

        var webApplicationFactory = new WebApplicationFactory<PaymentsController>();
        var client = webApplicationFactory.WithWebHostBuilder(builder =>
            builder.ConfigureServices(services => ((ServiceCollection)services)
                .AddSingleton<IPaymentsRepository>(paymentsRepository)
                .AddSingleton(httpClient)))
            .CreateClient();

        // Act
        var response = await client.GetAsync($"/api/Payments/{payment.Id}");
        var paymentResponse = await response.Content.ReadFromJsonAsync<PostPaymentResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(paymentResponse);
    }

    [Fact]
    public async Task Returns404IfPaymentNotFound()
    {
        // Arrange
        var webApplicationFactory = new WebApplicationFactory<PaymentsController>();
        var client = webApplicationFactory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/Payments/{Guid.NewGuid()}", CancellationToken.None);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }


    [Theory]
    [InlineData("2222405343248877", 2025, 4, 100, "GBP", "123", PaymentStatus.Authorized)]
    [InlineData("2222405343248112", 2026, 1, 60000, "USD", "456", PaymentStatus.Declined)]
    [InlineData("222240534324", 2025, 4, 100, "GBP", "123", PaymentStatus.Rejected)]
    public async Task PostsAPaymentSuccessfully(
        string cardNumber,
        int expiryYear,
        int expiryMonth,
        int amount,
        string currency,
        string cvv,
        PaymentStatus paymentStatus)
    {
        // Arrange
        var payment = new PostPaymentRequest
        {
            CardNumber = cardNumber,
            ExpiryYear = expiryYear,
            ExpiryMonth = expiryMonth,
            Amount = amount,
            Currency = currency,
            Cvv = cvv,
        };

        var paymentsRepository = new PaymentRepository();

        var webApplicationFactory = new WebApplicationFactory<PaymentsController>();
        var client = webApplicationFactory.WithWebHostBuilder(builder =>
            builder.ConfigureServices(services => ((ServiceCollection)services)
                .AddSingleton<IPaymentsRepository>(paymentsRepository)))
            .CreateClient();


        // Act
        var response = await client.PostAsJsonAsync($"/api/Payments", payment);
        var paymentResponse = await response.Content.ReadFromJsonAsync<PostPaymentResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(paymentResponse);
        Assert.Equal(paymentStatus,paymentResponse.Status);
    }
}