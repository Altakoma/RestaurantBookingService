﻿namespace IdentityService.DataAccess.Entities
{
    public class User : BaseEntity
    {
        public int UserRoleId { get; set; }

        public string Name { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;

        public UserRole UserRole { get; set; } = null!;
        public RefreshToken RefreshToken { get; set; } = null!;
    }
}
