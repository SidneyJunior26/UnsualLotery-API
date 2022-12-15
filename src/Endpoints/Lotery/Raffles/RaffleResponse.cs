using System;
namespace UnsualLotery.Endpoints.Lotery.Raffles;

public record RaffleResponse(Guid id, decimal Value, int AvailableQuantity, int TotalQuantity, bool Active, DateTime CreatedOn);