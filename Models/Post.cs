using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace Onker.Models; 

public class Post {
    public Guid PostId { get; set; }
    [Required] 
    public string Title { get; set; } = null!;
    
    public string? UrlLink { get; set; }

    public string? PostText { get; set; }
    
    public int PostKarma { get; set; } = 0;
    
    [ForeignKey(nameof(ApplicationUser))]
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser OriginalCommenter { get; set; } = null!;
    
    public List<Comment> Comments { get; set; }
    
    public Guid TagId { get; set; }
    public Tag Tag { get; set; }
}