using System;
using System.Collections.Generic;

#nullable disable

namespace CalendarTest.Models
{
    public partial class Team
    {
        public int Id { get; set; }
        public string CalendarStyle { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string DomainGroup { get; set; }
    }
}
