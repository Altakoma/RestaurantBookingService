﻿namespace IdentityService.DataAccess.Entities
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<User>? Users { get; set; }
    }
}