using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace Onker.Models; 

public class Comment {
    public Guid CommentId { get; set; }
    
    [Required] 
    public string CommentText { get; set; } = null!;
    public int CommentKarma { get; set; }
    
    [ForeignKey(nameof(ApplicationUser))]
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser OriginalCommenter { get; set; } = null;
}