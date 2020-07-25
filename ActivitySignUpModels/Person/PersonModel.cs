using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitySignUp.Models.Person
{
    public class PersonModel
    {
        public int PersonId { get; set; }
        public string PersonFirstName { get; set; }
        public string PersonLastName { get; set; }
        public string PersonEmail { get; set; }
        public int PersonActivityId { get; set; }
    }
}
