﻿using System;

namespace TheatreCMS3.Areas.Prod.Models
{
    public class Production
    {
        public int ProductionID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Playwright { get; set; }
        public int Runtime { get; set; }
        public DateTime OpeningDay { get; set; }
        public DateTime ClosingDay { get; set; }
        public DateTime ShowTimeEve { get; set; }
        public DateTime ShowTimeMat { get; set; }
        public int Season { get; set; }
        public bool IsWorldPremier { get; set; }
        public string TicketLink { get; set; }
        //public virtual List<CastMember> CastMembers { get; set; }
        //public virtual ProductionPhoto DefaultPhoto { get; set; }
        //public virtual List<ProductionPhoto> ProductionPhotos { get; set; }
        //public virtual List<CalendarEvent> CalendarEvents { get; set; }

        //public static bool IsCurrentlyShowing()
        //{
        //    return;
        //}
    }
}