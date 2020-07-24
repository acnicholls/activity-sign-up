using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitySignUp.Models.Activity
{
    public class ActivityViewModel
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime ActivityDateTime { get; set; }
        public string ActivityImage { get; set; }
    }
}
