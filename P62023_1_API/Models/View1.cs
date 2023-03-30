using System;
using System.Collections.Generic;

namespace P62023_1_API.Models
{
    public partial class View1
    {
        public int Expr1 { get; set; }
        public string UserRoleDescription { get; set; } = null!;
        public int UserStatusId { get; set; }
        public string UserStatuDescription { get; set; } = null!;
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string LoginPassword { get; set; } = null!;
        public string? CardId { get; set; }
        public string? Address { get; set; }
    }
}
