using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnsualLotery.Domain.Lotery;

public class Quota : Entity
{
    [ForeignKey("RaffleFk")]
    public Raffle Raffle { get; private set; }
    public Guid RaffleId { get; private set; }
    public int QuotasAmount { get; private set; }
    public string QuotasNumbers { get; private set; }
    public decimal Cost { get; private set; }
    public bool Paid { get; private set; }

    public Quota(Guid raffleId, int quotasAmount, string quotasNumbers, decimal cost,
        bool paid)
    {
        RaffleId = raffleId;
        QuotasAmount = quotasAmount;
        QuotasNumbers = quotasNumbers;
        Cost = cost;
        Paid = paid;
    }
}

