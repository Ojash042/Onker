using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onker.Services;
using System.Web.Http;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Onker.Controllers {

	public class APIController : ControllerBase {
		private readonly DataServices _dataServices;

		public APIController(DataServices dataServices) {
			_dataServices = dataServices;
		}

		[Route("api/GetAllTags")]
		public IActionResult GetAllTags() {
			var Result  =  _dataServices.GetAllTags().Result;
			var data = new {
				Message = Result
			};
			return Ok(data);

		}
	}
}

