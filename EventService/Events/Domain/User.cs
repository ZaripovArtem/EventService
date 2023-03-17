using Microsoft.AspNetCore.Identity;

namespace Features.Events.Domain;

public class User
{
    public Guid Id { get; set; }
    public string? NickName { get; set; }
}