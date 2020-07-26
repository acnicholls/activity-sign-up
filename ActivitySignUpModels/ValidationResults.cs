using System.Collections.Generic;
using System.Linq;

namespace ActivitySignUp.Models
{
    public class ValidationResults
    {
        public bool IsValid => !ValidationErrors.Any();

        public List<ValidationError> ValidationErrors { get; set; }
    }
}
