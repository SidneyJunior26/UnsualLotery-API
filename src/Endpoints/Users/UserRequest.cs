using System;
namespace UnsualLotery.Endpoints.Users;

public record UserRequest(string Email, string Password, string FirstName, string LastName, string PhoneNumber);