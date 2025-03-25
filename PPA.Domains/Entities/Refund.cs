using System;
using System.Collections.Generic;

namespace PPA.Domains.Entities;

public partial class Refund
{
    public int Id { get; set; }

    public DateTime RefundedAt { get; set; }

    public decimal RefundAmount { get; set; }

    public virtual ICollection<Cancelation> Cancelations { get; set; } = new List<Cancelation>();
}
