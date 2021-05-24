using System;
using System.Collections.Generic;

#nullable disable

namespace CalendarTest.Models
{
    public partial class Attachment
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByDisplayName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int EventId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedByDisplayName { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
