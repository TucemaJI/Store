﻿namespace Store.BusinessLogic.Models.Users
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool Confirmed { get; set; }// FOR WHAT?
        public bool? IsBlocked { get; set; }
    }
}
