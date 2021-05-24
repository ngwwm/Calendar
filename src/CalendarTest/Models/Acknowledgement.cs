using System;
using System.Collections.Generic;

#nullable disable

namespace CalendarTest.Models
{
    public partial class Acknowledgement
    {
        public int Id { get; set; }
        public string AckMessage { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByDisplayName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int EventId { get; set; }
        public string Team { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedByDisplayName { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Event Event { get; set; }
    }
}
