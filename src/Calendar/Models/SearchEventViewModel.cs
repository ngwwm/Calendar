using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Calendar.Models
{
    public class SearchEventViewModel
    {   
        public List<Event> Events { get; set; }
        public int SearchEventID { get; set; }
        public string SearchSubject { get; set; }
        public string SearchHost  { get; set; }
        public string SearchTeam {get; set;}
        public string SearchReference {get;set;} 
        public SelectList SearchByDate {get;set;} 
        public SelectList SearchByRange { get; set; }
        public string SearchRange {get; set;}
        public string SearchFromDate {get; set;}
        public string SearchToDate {get;set;} 
    }
}
