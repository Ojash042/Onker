using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Onker.Models;

namespace Onker.Data {
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid> {
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Tag> Tags { get; set; }


		public ApplicationDbContext(){

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<ApplicationUser>()
				.HasMany(user => user.Posts)
				.WithOne(posts => posts.OriginalPoster)
				.HasForeignKey(post => post.ApplicationUserId);

			modelBuilder.Entity<ApplicationUser>()
				.HasMany(user => user.Comments)
				.WithOne(comment => comment.OrigianalCommenter)
				.HasForeignKey(comment => comment.ApplicationUserId);
		}
	}
}
