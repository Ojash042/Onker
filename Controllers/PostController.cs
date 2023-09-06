using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Onker.Models;
using Onker.Services;
using OpenGraphNet;

namespace Onker.Controllers {
	public class PostController : Controller {
		private readonly DataServices _dataServices;
		public PostController(DataServices dataServices){
			_dataServices = dataServices;	
		}

		[Route("/Post/{id}")]
		public async Task<IActionResult> Index(string id) {
			Guid postId = Guid.Parse(id);
			var result = _dataServices.GetPost(postId).Result;

			List<string> tstList = new List<string>() { };
			result.HumanizedDate = result.Post.PostedDateTime.Humanize();
			result.HumanizedDateComments = new List<string>() { };
			foreach(var comment in result.Comments) {
				result.HumanizedDateComments.Add(comment.CommentedDateTime.Humanize());

			}

			if (result.Post.UrlLink.Length > 0) {
				OpenGraph openGraph = await OpenGraph.ParseUrlAsync(result.Post.UrlLink);

				string ogImage = "";
				string twitterImage = "";
				try {
					ogImage = openGraph.Metadata["og:image"].First().Value;
				}
				catch{}
				try {
					twitterImage = openGraph.Metadata["twitter:image"].First().Value;
				}
				catch { }
				result.URLMetadataImage = ogImage.Length > 0 ? ogImage : (twitterImage.Length > 0 ? twitterImage : "");
			}
			return View(result);
		}

		[Route("/Post/MakeComment")]
		[HttpPost]
		public IActionResult MakeComment() {
			Guid postId = Guid.Parse(Request.Form["PostId"]);
			Guid userId = Guid.Parse(Request.Form["UserId"]);
			string commentText = Request.Form["CommentText"];

			Comment newComment = new Comment() {
				CommentId = Guid.NewGuid(),
				CommentText = commentText,
				CommentedDateTime = DateTime.UtcNow,
				ApplicationUserId = userId,
				PostId = postId
			};
			_dataServices.CreateAComment(newComment);
			return Redirect($"/Post/{postId}");
		}
	}
}
