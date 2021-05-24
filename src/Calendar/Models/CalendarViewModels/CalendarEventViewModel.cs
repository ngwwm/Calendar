using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Calendar.Models.Services;

namespace Calendar.Models.CalendarViewModels
{
    /* CalendarEvent Class provided the data object used in the Calendar View */
    public class CalendarEventViewModel
    {
        public Event Event { get; set; }
        public DateTime OriginalStart { get; set; }
        public DateTime OriginalEnd { get; set; }
        public bool Continue { get; set; }
        public bool MarkedInCalendar { get; set; }
        public int ColorIdx { get; set; }
        public List<String> Projects { get; set; }
        public List<String> Teams { get; set; }
        public List<String> Servers { get; set; }
        public string EventStatus { get; set; }
        public string RiskLevel { get; set; }
        public int PrevEventID { get; set; }
        public int NextEventID { get; set; }
        public string DisplayAffectedHosts { get; set; }
        public string DisplayAffectedTeams { get; set; }
        public string DisplayAffectedProjects { get; set; }
        
        /* List of acknowledgements */
        public List<Acknowledgement> Acks { get; set; }

        /* List of attachments */
        public List<Attachment> Attachs { get; set; }

        // Constructor
        public CalendarEventViewModel(Event e) : this(e, null, null) { }
        public CalendarEventViewModel(Event e, List<Acknowledgement> acks, List<Attachment> attachs)
        {
            Event = e;
            OriginalStart = e.StartDateTime;
            OriginalEnd = e.EndDateTime;
            Continue = false;
            MarkedInCalendar = false;

            if (acks != null)
                Acks = acks;

            if (attachs != null)
                this.Attachs = attachs;

            DisplayAffectedHosts = e.AffectedHosts.Replace(",", ", ");
            DisplayAffectedTeams = e.AffectedTeams.Replace(",", ", ");
            DisplayAffectedProjects = e.AffectedProjects.Replace(",", ", ");

            Servers = e.AffectedHosts.Split(',').Select(p => p.Trim().ToUpper()).ToList();
            Projects = e.AffectedProjects.Split(',').Select(p => p.Trim().ToUpper()).ToList();
            Teams = e.AffectedTeams.Split(',').Select(p => p.Trim().ToUpper()).ToList();
            PrevEventID = 0;
            NextEventID = 0;

            // Lookup those Names by Values, such as RiskLevel, EventStatus, etc.
            //StaticListOfValuesService LOVs = StaticListOfValuesService;


            var es = StaticListOfValuesService.ListEventStatus().Where(m => m.Value == e.EventStatus);
            if (es.Count() == 1) { this.EventStatus = es.First().Name; } else { this.EventStatus = e.EventStatus; }
            var rl = StaticListOfValuesService.ListRiskLevels().Where(m => m.Value == e.RiskLevel);
            if (rl.Count() == 1) { this.RiskLevel = rl.First().Name; } else { this.RiskLevel = e.RiskLevel; }
        }
    }

}

