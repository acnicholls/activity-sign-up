using ActivitySignUp.Models;
using ActivitySignUp.Models.Activity;

namespace ActivitySignUp.Validation
{
    public class ActivityValidators
    {

        public ValidationResults ValidateInsertModel(ActivityInsertModel model)
        {
            var returnValue = new ValidationResults();
            if(model.ActivityName.Length > 50)
            {
                returnValue.ValidationErrors.Add(new ValidationError() { FieldName = "ActivityName", ErrorDetail = "The name is too long." });
            }
            if(model.ActivityDescription.Length > 250)
            {
                returnValue.ValidationErrors.Add(new ValidationError() { FieldName = "ActivityDescription", ErrorDetail = "The description is too long." });
            }

            return returnValue;

        }
    }
}
