using System;
using Flunt.Validations;
using UnsualLotery.Endpoints.Lotery;

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

        Validate();
    }

    private void Validate() {
        var contract = new Contract<Raffle>()
                    .IsGreaterThan(Value, 0, "value")
                    .IsGreaterThan(TotalQuantity, 0, "totalQuantity")
                    .IsGreaterThan(TotalQuantity, AvailableQuantity, "totalQuantity")
                    .AreEquals(AvailableQuantity, TotalQuantity, "availableQuantity", "Available Quantity most be equal Total Quantity")
                    .IsFalse(Active, "active");

        AddNotifications(contract);
    }
}

