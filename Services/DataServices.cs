using Microsoft.EntityFrameworkCore;
using Onker.Data;
using Onker.Models;

namespace Onker.Services {
	public class DataServices {
		private readonly ApplicationDbContext _dbContext;
		public DataServices(ApplicationDbContext dbContext) {
			_dbContext = dbContext;
		}


		public async Task<List<Tag>> GetAllTags() {
			return await _dbContext.Tags.ToListAsync();
		}

		public void SavePost(Post post) {
			_dbContext.Posts.Add(post);
			_dbContext.SaveChanges();
		}

		public void CreateTagObject(Tag tag) {
			_dbContext.Tags.Add(tag);
			_dbContext.SaveChanges();
		}
	}
}
