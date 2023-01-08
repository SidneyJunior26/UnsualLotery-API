using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Flunt.Validations;
using Flunt.Notifications;

namespace UnsualLotery.Endpoints.Lotery;

public record RaffleRequest(
    decimal Value,
    int AvailableQuantity,
    int TotalQuantity,
    bool Active
);