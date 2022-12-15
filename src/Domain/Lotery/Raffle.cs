using System;

namespace UnsualLotery.Domain.Lotery;

public class Raffle : Entity
{
    public decimal Value { get; private set; }
    public int AvailableQuantity { get; private set; }
    public int TotalQuantity { get; set; }
    public bool Active { get; private set; }
    public List<Quota> Quota { get; private set; }

    public Raffle(decimal value, int availableQuantity, int totalQuantity, bool active)
    {
        this.Value = value;
        this.AvailableQuantity = availableQuantity;
        this.TotalQuantity = totalQuantity;
        this.Active = active;
        this.CreatedBy = "User";
        this.CreatedOn = DateTime.Now;
        this.EditedBy = String.Empty;
        this.EditedOn = DateTime.MinValue;
    }
}

