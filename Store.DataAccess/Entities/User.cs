﻿using Microsoft.AspNetCore.Identity;
using System;

namespace Store.DataAccess.Entities
{ 
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
