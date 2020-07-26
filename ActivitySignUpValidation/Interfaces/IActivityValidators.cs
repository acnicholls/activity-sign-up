using ActivitySignUp.Models;
using ActivitySignUp.Models.Activity;

namespace ActivitySignUp.Validation.Interfaces
{
    public interface IActivityValidators
    {
        ValidationResults ValidateInsertModel(PersonInsertModel model);
    }
}
