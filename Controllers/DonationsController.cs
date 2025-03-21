using Amalgamate.Entity.Data;
using Amalgamate.Entity.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

[Route("api/donation")]
[ApiController]
public class DonationController : ControllerBase
{
    private readonly DonationService _donationService;
    private readonly AppDbContext _context;

    public DonationController(DonationService donationService, AppDbContext context)
    {
        _donationService = donationService;
        _context = context;
    }

    [HttpPost("create-checkout-session")]
    public ActionResult CreateCheckoutSession([FromBody] DonationDto donationDto)
    {
        try
        {
            var session = _donationService.CreateCheckoutSession(
                donationDto,
                "https://yourdomain.com/donation/success?sessionId={CHECKOUT_SESSION_ID}",
                "https://yourdomain.com/donation/cancel?sessionId={CHECKOUT_SESSION_ID}"

            );

            return Ok(new { sessionId = session.Id });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "");
        }
    }

}
