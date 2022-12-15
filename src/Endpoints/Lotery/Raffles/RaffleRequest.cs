using System;
namespace UnsualLotery.Endpoints.Lotery;

public record RaffleRequest(decimal Value, int AvailableQuantity, int TotalQuantity, bool Active);

