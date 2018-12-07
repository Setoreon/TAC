using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace events.tac.local.Models
{
    public class NavigationItem
    {
        public string Title { get; private set; }
        public string URL { get; private set; }
        public bool Active { get; private set; }

        public NavigationItem(string title, string uRL, bool active)
        {
            Title = title;
            URL = uRL;
            Active = active;
        }
    }
}