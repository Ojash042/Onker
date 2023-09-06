using Microsoft.EntityFrameworkCore;
using Onker.Data;
using Onker.Models;
using OpenGraphNet;

namespace Onker.Services {
	public class DataServices {
		private readonly ApplicationDbContext _dbContext;
		public DataServices(ApplicationDbContext dbContext) {
			_dbContext = dbContext;
		}


		public async Task<List<Tag>> GetAllTags() {
			return await _dbContext.Tags.ToListAsync();
		}

		public async Task<List<PostTagUser>> GetAllPosts() {
			var result = await _dbContext.Posts.Join(_dbContext.Tags,
				posts => posts.TagId,
				tags => tags.TagId,
				(posts, tags) => new {
					Post = posts,
					Tag = tags
				}).Join(_dbContext.Users,
				post => post.Post.ApplicationUserId,
				user => user.Id,
				(post, user) => new PostTagUser {
					Post = post.Post,
					Tag = post.Tag,
					User = user
				}).ToListAsync();
				return result;
		}

		public void CreateAComment(Comment comment) {
			_dbContext.Comments.Add(comment);
			_dbContext.SaveChanges();
		}
		public async Task<PostTagCommentsUser> GetPost(Guid postId) {
			return await _dbContext.Posts
				.Where(post => post.PostId == postId)
				.Include(post => post.Tag)
				.Include(post => post.Comments)
				.Select(post => new PostTagCommentsUser {
					Post = post,
					Tag = post.Tag,
					Comments = post.Comments,
					User = _dbContext.Users.FirstOrDefault(user => user.Posts.Any(post => post.PostId == postId))
				}).FirstOrDefaultAsync();
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
