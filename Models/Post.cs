using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Onker.Models {
	public class Post {
		public Guid PostId { get; set; }
		[Required]
		[StringLength(100,ErrorMessage ="Title should be atleast {2} and at max {1} character long",MinimumLength =10)]
		public string Title { get; set; } = null!;
		[AllowNull]
		public string? UrlLink { get; set; }
		[AllowNull]
		public string? PostText { get; set; }
		public int PostKarma { get; set; } = 0;
		public DateTime PostedDateTime { get; set; } = DateTime.UtcNow;

		public ICollection<Comment> Comments { get; set; }

		public Guid TagId { get; set; }
		public Tag Tag { get; set; }

		[ForeignKey(nameof(ApplicationUser))]
		public Guid ApplicationUserId { get; set; }
		public ApplicationUser OriginalPoster { get; set; }

	}
}
