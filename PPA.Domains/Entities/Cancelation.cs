using System;
using System.Collections.Generic;

namespace PPA.Domains.Entities;

public partial class Cancelation
{
    public int Id { get; set; }

    public DateTime CanceledAt { get; set; }

    public string? Reason { get; set; }

    public int? Refund { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Refund? RefundNavigation { get; set; }
}
