﻿using Moor.Model.Models.Base;

namespace Moor.Model.Model.Authorize
{
    public class PersonnelModel
    {
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? MediaPath { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? PasswordAgain { get; set; }
        public long? RoleId { get; set; }
    }
}
