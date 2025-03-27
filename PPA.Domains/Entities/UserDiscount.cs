using System;
using System.Collections.Generic;

namespace PPA.Domains.Entities;

public partial class UserDiscount
{
    public int Id { get; set; }

    public string User { get; set; } = null!;

    public int Discount { get; set; }

    public bool Used { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Discount DiscountNavigation { get; set; } = null!;

    public virtual AspNetUser UserNavigation { get; set; } = null!;
}
