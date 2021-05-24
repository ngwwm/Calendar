using System;
using System.Collections.Generic;

#nullable disable

namespace CalendarTest.Models
{
    public partial class Event
    {
        public Event()
        {
            Acknowledgements = new HashSet<Acknowledgement>();
        }

        public int Id { get; set; }
        public string AffectedHosts { get; set; }
        public string AffectedProjects { get; set; }
        public string Category { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Reference { get; set; }
        public string Result { get; set; }
        public DateTime StartDateTime { get; set; }
        public string Subject { get; set; }
        public string TaskDescription { get; set; }
        public string AffectedTeams { get; set; }
        public string ActionBy { get; set; }
        public string Environment { get; set; }
        public string HealthCheckBy { get; set; }
        public string Impact { get; set; }
        public string Likelihood { get; set; }
        public string RiskLevel { get; set; }
        public string EventStatus { get; set; }
        public string FallbackProcedure { get; set; }
        public string MaintProcedure { get; set; }
        public string VerificationStep { get; set; }
        public string ImpactAnalysis { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedByDisplayName { get; set; }
        public string UpdatedByDisplayName { get; set; }
        public string RiskAnalysis { get; set; }

        public virtual ICollection<Acknowledgement> Acknowledgements { get; set; }
    }
}
