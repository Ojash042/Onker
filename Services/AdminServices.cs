using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Onker.Data;
using Onker.Models;

namespace Onker.Services {
	public class AdminServices {
		private readonly ApplicationDbContext _dbContext;
		private readonly RoleManager<IdentityRole<Guid>> _roleManager;

		public AdminServices(ApplicationDbContext dbContext, RoleManager<IdentityRole<Guid>> roleManager) {
			_dbContext = dbContext;
			_roleManager = roleManager;
		}

		public async Task<List<Tag>> GetAllTags() {
			return await _dbContext.Tags.ToListAsync();	
		}

		public void CreateATag(Tag tag) {
			_dbContext.Tags.Add(tag);
			_dbContext.SaveChanges();
		}

		public async Task<List<IdentityRole<Guid>>> GetAllUserRoles() {
			return await _roleManager.Roles.ToListAsync();	
		}
		
		public async Task AddUserRoles(string role) {
			await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
		}

		public async Task<List<ApplicationUser>> GetAllUsers() {
			return await _dbContext.Users.ToListAsync();
		}

		public void CreateAPost(Post post) {
			_dbContext.Posts.Add(post);
			_dbContext.SaveChanges();
		}

	}
}
