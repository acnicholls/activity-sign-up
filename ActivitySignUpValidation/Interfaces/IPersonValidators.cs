using ActivitySignUp.Models;
using ActivitySignUp.Models.Person;

namespace ActivitySignUp.Validation.Interfaces
{
    public interface IPersonValidators
    {
        ValidationResults ValidateInsertModel(PersonInsertModel model);
    }
}
