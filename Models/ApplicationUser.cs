using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Onker.Models {
	public class ApplicationUser : IdentityUser<Guid>{
		//[Required]
		//public string Username { get; set; } = null!;
		public DateTime DateJoined { get; set; } = DateTime.UtcNow;
		public int UserKarma { get; set; } = 0;

		public ICollection<Post> Posts { get; set; }
		public ICollection<Comment> Comments { get; set; }

		
	}
}
