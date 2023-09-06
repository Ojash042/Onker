using Microsoft.AspNetCore.Identity;

namespace Onker.Models {
	public class UserView {
		public ApplicationUser user { get; set; }
		public List<IdentityRole<Guid>> Roles { get; set; }
		public string RoletoBeAdded { get; set; }
	}
}
