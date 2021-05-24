using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Calendar.Models
{
    public class EventViewModel
    {
        public Event E { get; set;  }

        public IEnumerable<SelectListItem> RiskLevelMatrix { get; set; }
        public IEnumerable<SelectListItem> EventStatus { get; set; }
        public IEnumerable<SelectListItem> Environments { get; set; }

        public IEnumerable<SelectListItem> Likelihoods { get; set; }
        public IEnumerable<SelectListItem> Impacts { get; set; }
        public IEnumerable<SelectListItem> RiskLevels { get; set; }
        public IEnumerable<SelectListItem> Teams { get; set; }
        public IEnumerable<SelectListItem> Projects { get; set; }


    }
}
