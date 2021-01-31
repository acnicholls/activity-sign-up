using System.Collections.Generic;
using System.Linq;

namespace ActivitySignUp.Models
{
    public class ValidationResults
    {
        public ValidationResults()
        {
            ValidationErrors = new List<ValidationError>();
        }

        public bool IsValid { get { return !ValidationErrors.Any(); } } 

        public List<ValidationError> ValidationErrors { get; set; }
    }
}
