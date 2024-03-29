﻿using System;
using System.Collections.Generic;

namespace P62023_1_API.Models
{
    public partial class AppointmentStatus
    {
        public AppointmentStatus()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int AppoStatusId { get; set; }
        public string AppoStatusDescription { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
