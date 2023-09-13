using Microsoft.Build.Framework;

namespace Onker.Models; 

public class Tag {
    public Guid TagId { get; set; }

    [Required]
    public string TagName { get; set; } = null!;

    public List<Post> Posts { get; set; }
}