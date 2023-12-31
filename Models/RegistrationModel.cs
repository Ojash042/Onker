﻿using Azure.Identity;
using System.ComponentModel.DataAnnotations;

namespace Onker.Models {
	public class RegistrationModel {
		[EmailAddress]
		[Required(ErrorMessage ="E-Mail Address is required")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Username is required")]
		public string? UserName { get; set; }
		[Required(ErrorMessage = "Password is required")]
		public string? Password { get; set; } 
	}
}
