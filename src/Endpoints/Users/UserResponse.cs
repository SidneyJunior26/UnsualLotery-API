using System;
namespace UnsualLotery.Endpoints.Users;

public record UserResponse(string FirstName, string LastName, string Email, string PhoneNumber);