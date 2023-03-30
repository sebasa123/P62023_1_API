using System;
using System.Collections.Generic;

namespace P62023_1_API.Models
{
    public partial class RecoveryCode
    {
        public int RecoveryCodeId { get; set; }
        public string Email { get; set; } = null!;
        public string RecoveryCode1 { get; set; } = null!;
        public DateTime GenerateDate { get; set; }
        public bool IsUsed { get; set; }
    }
}
