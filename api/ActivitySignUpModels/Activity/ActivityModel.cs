using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitySignUp.Models.Activity
{
    public class ActivityModel
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime ActivityDateTime { get; set; }
        public byte[] ActivityImage { get; set; }
    }
}
