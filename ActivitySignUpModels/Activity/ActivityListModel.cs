using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitySignUp.Models.Activity
{
    public class ActivityListModel
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public DateTime ActivityDateTime { get; set; }
        public string ActivityImage { get; set; }
    }
}
