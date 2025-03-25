using System;
using System.Collections.Generic;

namespace PPA.Domains.Entities;

public partial class Discount
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal DiscountPercentage { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<CityDiscount> CityDiscounts { get; set; } = new List<CityDiscount>();
}
