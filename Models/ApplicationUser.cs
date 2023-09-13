using Microsoft.AspNetCore.Identity;

namespace Onker.Models; 

public class ApplicationUser : IdentityUser<Guid> {
    public string Username { get; set; } = null!;
    public DateTime DateJoined { get; set; } = DateTime.UtcNow;
    public Int16 Karma { get; set; } = 0;
    public List<Post> Posts { get; set; }
    public List<Comment> Comments { get; set; }
}