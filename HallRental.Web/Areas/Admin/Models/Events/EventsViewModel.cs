
namespace HallRental.Web.Areas.Admin.Models.Events
{
    using System.Collections.Generic;

    public class EventsViewModel
    {
        public IEnumerable<EventsListModel> Events { get; set; }

        public string Search { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}
