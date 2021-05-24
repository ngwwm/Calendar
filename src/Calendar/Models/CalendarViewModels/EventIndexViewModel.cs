using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Calendar.Models.CalendarViewModels
{
    public class EventIndexData
    {
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<Acknowledgement> Acknowledgements { get; set; }
        public string SearchSort { get; set; }
        public string SearchEventID { get; set; }
        public string SearchSubject { get; set; }
        public string SearchHost { get; set; }
        public string SearchProject { get; set; }
        public string SearchTeam { get; set; }
        public string SearchReference { get; set; }
        public string SearchByDate{get; set;}
        public string SearchByRange { get; set; }
        public SelectList SearchByDateList {get;set;}
        public SelectList SearchByRangeList {get; set;}
        public string SearchFromDate {get; set;}
        public string SearchToDate {get;set;} 
    }
}
