using Stripe;
using Stripe.Checkout;
using Microsoft.Extensions.Options;
using Amalgamate.Entity.Entities;
using Amalgamate.Entity.Dto;
using System;
using Amalgamate.Entity.Data;
using Amalgamate.Entity;

public class DonationService
{
    private readonly AppDbContext _context;

    public DonationService(IOptions<StripeSettings> stripeSettings, AppDbContext context)
    {
        StripeConfiguration.ApiKey = stripeSettings.Value.SecretKey;
        _context = context;
    }

    public Session CreateCheckoutSession(DonationDto donationDto, string successUrl, string cancelUrl)
    {
        try
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = donationDto.Amount,
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Donation",
                                Description = donationDto.Description
                            },
                        },
                        Quantity = 1,
                    },
                },  
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
            };

            var service = new SessionService();
            Session session = service.Create(options);

            var donation = new Donation
            {
                Name = donationDto.Name,
                Email = donationDto.Email,
                PhoneNumber= donationDto.PhoneNumber,
                Amount = donationDto.Amount,
                Description = donationDto.Description,
                StripeSessionId=session.Id,
                CreatedAt = DateTime.UtcNow
            };

            _context.Donations.Add(donation);
            _context.SaveChanges();

            return session;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error");
            return null;
        }
    }
}
