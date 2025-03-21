using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amalgamate.Entity.Entities
{
    public class Donation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public string StripeSessionId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
