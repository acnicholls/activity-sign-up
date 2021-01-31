using System;

namespace ActivitySignUp.Models.Activity
{
    public class ActivityListModel
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public DateTime ActivityDateTime { get; set; }
        public byte[] ActivityImage { get; set; }
    }
}
