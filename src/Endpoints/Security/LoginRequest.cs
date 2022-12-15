using System;
namespace UnsualLotery.Endpoints.Security;

public record LoginRequest(string Email, string Password);