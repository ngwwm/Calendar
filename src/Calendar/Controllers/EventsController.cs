using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Calendar.Data;
using Calendar.Models;
using Calendar.Models.CalendarViewModels;
using Calendar.Helpers;
using Calendar.Models.Services;

namespace Calendar.Controllers
{
    public class EventsController : Controller
    {
        const int DAYSINAWEEK = 7;
        private readonly ApplicationDbContext _context;


        public EventsController(ApplicationDbContext context)
        {
            _context = context;    
        }


        // GET: Events
        //startdate_desc, startdate_asce, creation_desc, creation_asce
        public async Task<IActionResult> Index(string sort, string eventid, string subject, string searchrange, string searchday, string host, string project, string team, string refid, string searchdatefrom, string searchdateto, int? page, int? pgsize)
        {
            ViewBag.SortParm = String.IsNullOrEmpty(sort) ? "" : sort;
            ViewBag.SubjectParm = String.IsNullOrEmpty(subject) ? "" : subject;
            ViewBag.HostParm = String.IsNullOrEmpty(host) ? "" : host;
            ViewBag.ProjParm = String.IsNullOrEmpty(project) ? "" : project;
            ViewBag.TeamParm = String.IsNullOrEmpty(team) ? "" : team;
            ViewBag.RefParm = String.IsNullOrEmpty(refid) ? "" : refid;
            ViewBag.RangeParm = searchrange;
            ViewBag.DayParm = searchday;
            ViewBag.EventParm = String.IsNullOrEmpty(eventid) ? "" : eventid;
            ViewBag.FromParm = String.IsNullOrEmpty(searchdatefrom) ? "" : searchdatefrom;
            ViewBag.ToParm = String.IsNullOrEmpty(searchdateto) ? "" : searchdateto;

            if (String.IsNullOrEmpty(eventid) && String.IsNullOrEmpty(subject)
                && String.IsNullOrEmpty(host) && String.IsNullOrEmpty(project)
                && String.IsNullOrEmpty(team) && String.IsNullOrEmpty(refid)
                && (String.IsNullOrEmpty(searchday) || searchday.Equals("ND")))
            {
                ViewBag.SearchOn = "";
            } else
            {
                ViewBag.SearchOn = "in";
            }

            var events = from e in _context.Event
                         select e;

            int n_eventid;
            if (!String.IsNullOrEmpty(eventid) && Int32.TryParse(eventid, out n_eventid))
            {
                events = events.Where(e => e.ID.Equals(n_eventid));
            }

            if (!String.IsNullOrEmpty(subject))
            {
                events = events.Where(e => e.Subject.Contains(subject));
            }
            if (!String.IsNullOrEmpty(host))
            {
                events = events.Where(e => e.AffectedHosts.Contains(host));
            }
            if (!String.IsNullOrEmpty(project))
            {
                events = events.Where(e => e.AffectedProjects.Contains(project));
            }
            if (!String.IsNullOrEmpty(team))
            {
                events = events.Where(e => e.AffectedTeams.Contains(team));
            }
            if (!String.IsNullOrEmpty(refid))
            {
                events = events.Where(e => e.Reference.Contains(refid));
            }

            DateTime datetimefrom;
            DateTime datetimeto;

            if (!String.IsNullOrEmpty(searchdatefrom) || !String.IsNullOrEmpty(searchdateto))
            {

                if (searchday.Equals("SD"))
                {
                    if (!String.IsNullOrEmpty(searchdatefrom) && DateTime.TryParse(searchdatefrom, out datetimefrom))
                    {
                        events = events.Where(e => e.StartDateTime >= datetimefrom);
                    }
                    if (!String.IsNullOrEmpty(searchdateto) && DateTime.TryParse(searchdateto, out datetimeto))
                    {
                        events = events.Where(e => e.EndDateTime <= datetimeto);
                    }
                }
                else if (searchday.Equals("CD"))
                {
                    if (!String.IsNullOrEmpty(searchdatefrom) && DateTime.TryParse(searchdatefrom, out datetimefrom))
                    {
                        events = events.Where(e => e.CreatedDate >= datetimefrom);
                    }
                    if (!String.IsNullOrEmpty(searchdateto) && DateTime.TryParse(searchdateto, out datetimeto))
                    {
                        events = events.Where(e => e.CreatedDate <= datetimeto);
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(searchdatefrom) && DateTime.TryParse(searchdatefrom, out datetimefrom))
                    {
                        events = events.Where(e => e.UpdatedDate >= datetimefrom);
                    }
                    if (!String.IsNullOrEmpty(searchdateto) && DateTime.TryParse(searchdateto, out datetimeto))
                    {
                        events = events.Where(e => e.UpdatedDate <= datetimeto);
                    }
                }
            }
            else
            {
                switch (searchrange)
                {
                    case "S1":
                        events = events.Where(e => e.StartDateTime.Date >= DateTime.Now.Date && e.StartDateTime.Date <= (DateTime.Now.AddDays(1).Date));
                        break;
                    case "S2":
                        events = events.Where(e => e.StartDateTime.Date >= DateTime.Now.Date && e.StartDateTime.Date <= (DateTime.Now.AddDays(7).Date));
                        break;
                    case "S3":
                        events = events.Where(e => e.StartDateTime.Date >= DateTime.Now.Date && e.StartDateTime.Date <= (DateTime.Now.AddDays(30).Date));
                        break;
                    case "S4":
                        events = events.Where(e => e.StartDateTime.Date >= DateTime.Now.Date && e.StartDateTime.Date <= (DateTime.Now.AddDays(90).Date));
                        break;
                    case "C1":
                        events = events.Where(e => e.CreatedDate.Date >= DateTime.Now.Date && e.CreatedDate.Date <= (DateTime.Now.AddDays(1).Date));
                        break;
                    case "C2":
                        events = events.Where(e => e.CreatedDate.Date >= (DateTime.Now.AddDays(-7).Date) && e.CreatedDate.Date <= DateTime.Now.Date);
                        break;
                    case "C3":
                        events = events.Where(e => e.CreatedDate.Date >= (DateTime.Now.AddDays(-30).Date) && e.CreatedDate.Date <= DateTime.Now.Date);
                        break;
                    case "C4":
                        events = events.Where(e => e.CreatedDate.Date >= (DateTime.Now.AddDays(-90).Date) && e.CreatedDate.Date <= DateTime.Now.Date);
                        break;
                    case "U1":
                        events = events.Where(e => e.UpdatedDate.Date >= DateTime.Now.Date && e.UpdatedDate.Date <= (DateTime.Now.AddDays(1).Date));
                        break;
                    case "U2":
                        events = events.Where(e => e.UpdatedDate.Date >= (DateTime.Now.AddDays(-7).Date) && e.UpdatedDate.Date <= DateTime.Now.Date);
                        break;
                    case "U3":
                        events = events.Where(e => e.UpdatedDate.Date >= (DateTime.Now.AddDays(-30).Date) && e.UpdatedDate.Date <= DateTime.Now.Date);
                        break;
                    case "U4":
                        events = events.Where(e => e.UpdatedDate.Date >= (DateTime.Now.AddDays(-90).Date) && e.UpdatedDate.Date <= DateTime.Now.Date);
                        break;
                    default:
                        break;
                }
            }

            switch (sort)
            {
                case "cd_a":
                    events = events.OrderBy(e => e.CreatedDate);
                    break;
                case "cd_d":
                    events = events.OrderByDescending(e => e.CreatedDate);
                    break;
                case "ud_a":
                    events = events.OrderBy(e => e.UpdatedDate);
                    break;
                case "ud_d":
                    events = events.OrderByDescending(e => e.UpdatedDate);
                    break;
                case "sd_a":
                    events = events.OrderBy(e => e.StartDateTime);
                    break;
                default:   // sd_d
                    events = events.OrderByDescending(e => e.StartDateTime);
                    break;
            }

            var viewModel = new EventIndexData {
                Events = events
                  .Include(e => e.Acknowledgements)
                  .AsNoTracking(),
                SearchSort = sort,
                SearchEventID = eventid,
                SearchByRange = searchrange,
                SearchByDate = searchday,
                SearchByRangeList = new SelectList(StaticListOfValuesService.ListSearchRange()),
                SearchByDateList = new SelectList(StaticListOfValuesService.ListSearchDate()),
                SearchSubject = subject,
                SearchHost = host,
                SearchTeam = team,
                SearchProject = project,
                SearchFromDate = searchdatefrom,
                SearchToDate = searchdateto,
                SearchReference = refid,

        };
            return View(new PaginatedEventIndex(viewModel, page ?? 1, pgsize ?? 30));

            //return View(await PaginatedList<Event>.CreateAsync(events.AsNoTracking(), page ?? 1, pgsize ?? 30));

            //return View(await events.AsNoTracking().ToListAsync());
            //return View(await _context.Event.OrderByDescending(m => m.StartDateTime).ToListAsync());
        }


        // GET: Events/Calendar
        public async Task<IActionResult> Calendar(int? year, int? month, int? reviewEventId, int? reviewDirection, string filterProject, string filterTeam)
        {
            DateTime now = System.DateTime.Now;
            DateTime ViewDate;

            if (year == null)
            {
                year = now.Year;
                month = now.Month;
            } else if (month == null)
            {
                month = now.Month;                
            }

            try
            {
                ViewDate = new DateTime((int)year, (int)month, 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }

            /* Prefer using ViewBag instead of VieData */
            ViewBag.ViewDate = ViewDate;
            /* If event id is present, we know that we are review events in modal */
            ViewBag.ReviewEventId   = reviewEventId;
            /* Revie direction give us a hint where are we going */
            if (reviewDirection == null)               
                ViewBag.ReviewDirection = 0;
            else
                ViewBag.ReviewDirection = reviewDirection;

            DateTime FirstDateofTheMonth  = new DateTime(ViewDate.Year, ViewDate.Month, 1);
            DateTime FirstDateofNextMonth = new DateTime(ViewDate.Year, ViewDate.Month, 1).AddMonths(1);
            DateTime LastDateofTheMonth   = new DateTime(ViewDate.Year, ViewDate.Month, 1).AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

            DateTime FirstDateOfTheCalendar = FirstDateofTheMonth.AddDays((int)FirstDateofTheMonth.DayOfWeek * -1);
            DateTime LastDateOfTheCalendar = LastDateofTheMonth.Date.AddDays(DAYSINAWEEK - 1 - (int)LastDateofTheMonth.DayOfWeek).AddHours(23).AddMinutes(59).AddSeconds(59);

            var e = from m in _context.Event.Where(m => !(m.EndDateTime <= FirstDateOfTheCalendar
                                                            || m.StartDateTime >= LastDateOfTheCalendar))
                         select m;

            if (!String.IsNullOrEmpty(filterProject))
            {
                ViewBag.FilterProject = filterProject.ToUpper();
                /* filter will be done in View */
                //e = e.Where(m => m.AffectedProjects.Contains(filterProject.ToUpper()));                                             
            }

            if (!String.IsNullOrEmpty(filterTeam))
            {
                ViewBag.FilterTeam = filterTeam.ToUpper();
                /* filter will be done in View */
                //e = e.Where(m => m.AffectedTeams.Contains(filterTeam.ToUpper()));
            }

            var @events = await e.OrderBy(m => m.StartDateTime).ToListAsync();
            if (@events == null)
            {
                return NotFound();
            }

            /* To maintain separation of concerns in mvc. */
            List<CalendarEventViewModel> CalEvents = new List<CalendarEventViewModel>();

            int idx = 0;
            int eventCount = @events.Count();

            foreach (var item in @events)
            {
                CalendarEventViewModel ce = new CalendarEventViewModel(item);

                /* we need to trim the startdate and enddate */
                if (ce.Event.StartDateTime < FirstDateOfTheCalendar)
                {
                    ce.Event.StartDateTime = FirstDateOfTheCalendar;
                    ce.Continue = true;
                }

                if (ce.Event.EndDateTime > LastDateOfTheCalendar)
                {
                    ce.Event.EndDateTime = LastDateOfTheCalendar;
                }

                CalEvents.Add(ce);

                /* Now, workout the PrevEventID and NextEventID for ReviewEventModal */
                if (idx > 0 && idx < eventCount)
                {
                    CalEvents[idx].PrevEventID = CalEvents[idx - 1].Event.ID;
                    CalEvents[idx - 1].NextEventID = CalEvents[idx].Event.ID;
                }
                idx++;
            }

            return View(CalEvents);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id, string modal = null, string redir = null)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var @event = await _context.Event.SingleOrDefaultAsync(m => m.ID == id);

            ViewBag.Redir = redir;
            ViewBag.Modal = modal;  /* show event detail with modal */

            if (@event == null)
            {
                return NotFound();
            }

            var acks = await _context.Acknowledgement.Where(m => m.EventID == id).OrderBy(m => m.UpdatedDate).ToListAsync();

            var attachs = await _context.Attachment.Where(m => m.EventID == id).OrderBy(m => m.UpdatedDate).ToListAsync();

            CalendarEventViewModel CalendarEvent = new CalendarEventViewModel(@event, acks, attachs);
            /*
            var attachs = await _context.Attachment.Where(m => m.EventID == id).OrderBy(m => m.UpdatedDate).ToListAsync();
            CalendarEvent = new CalendarEventViewModel(@event, null, attachs);
            */
            return View(CalendarEvent);
        }

        // GET: Events/Create
        public async Task<IActionResult> Create(string redir = null, string date = null)
        {
            ViewBag.Title = "Create Event";
            ViewBag.Redir = redir;

            if (User.IsInRole(Constants.ROLE_ADMIN))
            {
                var viewModel = new EventViewModel
                {
                    E = new Event(),
                    EventStatus = new SelectList(StaticListOfValuesService.ListEventStatus(), "Value", "Name"),
                    Likelihoods = new SelectList(StaticListOfValuesService.ListLikelihoods(), "Value", "Name"),
                    RiskLevels = new SelectList(StaticListOfValuesService.ListRiskLevels(), "Value", "Name"),
                    RiskLevelMatrix = new SelectList(StaticListOfValuesService.ListRiskLevels(), "Value", "Name"),
                    Impacts = new SelectList(StaticListOfValuesService.ListImpacts(), "Value", "Name"),
                    Environments = new SelectList(StaticListOfValuesService.ListEnvironments(), "Value", "Name")
                };
                if (date != null && date != "")
                {
                    viewModel.E.StartDateTime = DateTime.ParseExact(date, "d/m/yyyy", null);
                    viewModel.E.EndDateTime = viewModel.E.StartDateTime.AddHours(12);
                }

                viewModel.Teams = new SelectList(await _context.Team.ToListAsync(), "ID", "Name");
                viewModel.Projects = new SelectList(await _context.Project.ToListAsync(), "ID", "Name");

                return View(viewModel);
            }
            else
                return NotFound();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AffectedHosts,AffectedProjects,Category,EndDateTime,Reference,Result,StartDateTime,Subject,TaskDescription,AffectedTeams,RiskLevel,Environment,ActionBy,HealthCheckBy,Likelihood,Impact,ImpactAnalysis,MaintProcedure,VerificationStep,FallbackProcedure,EventStatus,RiskAnalysis")] Event @event, string redir = null)
        {
            ViewBag.Title = "Create Event";
            ViewBag.Redir = redir;
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (ModelState.IsValid)
            {
                /* Audit Fields */
                var username = "anonymous";
                var u = User.Claims.Where(m => m.Type == "username");
                if (u.Count() == 1) { username = u.First().Value; }
                var displayname = "anonymous";
                var d = User.Claims.Where(m => m.Type == "displayName");
                if (d.Count() == 1) { displayname = d.First().Value; }
                @event.CreatedDate = DateTime.Now;
                @event.CreatedBy = username;
                @event.CreatedByDisplayName = displayname;
                @event.UpdatedDate = @event.CreatedDate;
                @event.UpdatedBy = username;
                @event.UpdatedByDisplayName = displayname;

                _context.Add(@event);
                await _context.SaveChangesAsync();
                if (redir == null)
                    return RedirectToAction("Index");
                else
                    return Redirect(redir);
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        /*
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.SingleOrDefaultAsync(m => m.ID == id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }
        */

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id, string redir = null)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.SingleOrDefaultAsync(m => m.ID == id);

            if (@event == null)
            {
                return NotFound();
            }
            var viewModel = new EventViewModel
            {
                E = @event,
                EventStatus = new SelectList(StaticListOfValuesService.ListEventStatus(), "Value", "Name"),
                Likelihoods = new SelectList(StaticListOfValuesService.ListLikelihoods(), "Value", "Name"),
                RiskLevels = new SelectList(StaticListOfValuesService.ListRiskLevels(), "Value", "Name"),
                RiskLevelMatrix = new SelectList(StaticListOfValuesService.ListRiskLevels(), "Value", "Name"),
                Impacts = new SelectList(StaticListOfValuesService.ListImpacts(), "Value", "Name"),
                Environments = new SelectList(StaticListOfValuesService.ListEnvironments(), "Value", "Name")
            };

            viewModel.Teams = new SelectList(await _context.Team.ToListAsync(), "ID", "Name");
            viewModel.Projects = new SelectList(await _context.Project.ToListAsync(), "ID", "Name");

            ViewBag.Redir = redir;
            return View(viewModel);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AffectedHosts,AffectedProjects,Category,EndDateTime,Reference,Result,StartDateTime,Subject,TaskDescription,AffectedTeams,RiskLevel,Environment,ActionBy,HealthCheckBy,Likelihood,Impact,ImpactAnalysis,MaintProcedure,VerificationStep,FallbackProcedure,EventStatus,CreatedDate,CreatedBy,CreatedByDisplayName,RiskAnalysis")] Event @event, string redir = null)
        {
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (id != @event.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /* Audit Fields */
                    var username = "anonymous";
                    var u = User.Claims.Where(m => m.Type == "username");
                    if (u.Count() == 1) { username = u.First().Value; }
                    var displayname = "anonymous";
                    var d = User.Claims.Where(m => m.Type == "displayName");
                    if (d.Count() == 1) { displayname = d.First().Value; }
                    @event.UpdatedDate = DateTime.Now;
                    @event.UpdatedBy = username;
                    @event.UpdatedByDisplayName = displayname;

                    //The IsModified doesn't work!
                    //_context.Entry(@event).Property("CreatedBy").IsModified = false;
                    //_context.Entry(@event).Property("CreatedByDisplayName").IsModified = false;
                    //_context.Entry(@event).Property("CreatedDate").IsModified = false;

                    _context.Update(@event);
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (redir != null )
                    return Redirect(redir);
                else
                    return RedirectToAction("Index");                
/*
 
                //string sredir = redir.Replace("%3F", "?").Replace("%3D","=").Replace("%26","&");
                //string sredir;
                //string pYear = "", pMonth = "";

                if (redir != null && redir.StartsWith("Calendar"))
                {
                    sredir = Uri.UnescapeDataString(redir);
                    int qPos = sredir.IndexOf('?');
                    if (qPos >= 0)
                    {
                        sredir = sredir.Substring(qPos + 1, sredir.Length - (qPos + 1));
                    }
                    string[] sparam = sredir.Split('&');

                    foreach (var p in sparam)
                    {
                        string[] svalue = p.Split('=');

                        if (svalue[0].ToLower() == "year")
                            pYear = svalue[1];
                        if (svalue[0].ToLower() == "month")
                            pMonth = svalue[1];
                    }

                    RedirectToActionResult redirectResult = new RedirectToActionResult("Calendar", "Events", new { @year = pYear, @month = pMonth });
                    //return RedirectToAction(Uri.UnescapeDataString(redir));
                    //return RedirectToAction(sredir);                
                    return redirectResult;
                }
                else
                    return RedirectToAction("Index");
*/
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id, string redir = null)
        {

            ViewBag.Redir = redir;

            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.SingleOrDefaultAsync(m => m.ID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string redir = null)
        {
            var @event = await _context.Event.SingleOrDefaultAsync(m => m.ID == id);
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();

            ViewBag.Redir = redir;
            if (redir == null)
                return RedirectToAction("Index");
            else
                return Redirect(redir);
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.ID == id);
        }

        // GET: Events/Copy/5
        public async Task<IActionResult> Copy(int? id, string redir = null)
        {
            ViewBag.Title = "Copy Event";
            ViewBag.Redir = redir;
            if (!User.IsInRole(Constants.ROLE_ADMIN))
                return NotFound();

            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.SingleOrDefaultAsync(m => m.ID == id);
            if (@event == null)
            {
                return NotFound();
            }
            return View("Create", @event);
        }        
    }
}
