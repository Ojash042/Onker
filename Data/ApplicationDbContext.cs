using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Onker.Models;

namespace Onker.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid> {
   public DbSet<Post> Posts { get; set; }
   public DbSet<Comment> Comments { get; set; }
   public DbSet<Tag> Tags { get; set; }


      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {

   }

   protected override void OnModelCreating(ModelBuilder builder) {
      base.OnModelCreating(builder);
      builder.Entity<ApplicationUser>()
         .HasMany(user => user.Posts)
         .WithOne(post => post.OriginalCommenter)
         .IsRequired();

      builder.Entity<ApplicationUser>()
         .HasMany(user => user.Comments)
         .WithOne(comment => comment.OriginalCommenter)
         .IsRequired();

   }
}