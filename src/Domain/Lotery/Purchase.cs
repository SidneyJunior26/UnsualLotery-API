using System;

namespace UnsualLotery.Domain.Lotery;

public class Purchase : Entity
{
    public decimal Value { get; private set; }
    public int QuotasAmount { get; private set; }
    public bool Paid { get; set; }

    public Purchase(decimal value, int quotasAmount, bool paid)
    {
        Value = value;
        QuotasAmount = quotasAmount;
        Paid = paid;
    }
}

