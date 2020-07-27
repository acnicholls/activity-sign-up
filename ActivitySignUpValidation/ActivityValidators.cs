using ActivitySignUp.Models;
using ActivitySignUp.Models.Activity;
using ActivitySignUp.Validation.Interfaces;

namespace ActivitySignUp.Validation
{
    public class ActivityValidators : IActivityValidators
    {
        public ValidationResults ValidateInsertModel(ActivityInsertModel model)
        {
            var returnValue = new ValidationResults();
            if(model == null)
            {
                returnValue.ValidationErrors.Add(new ValidationError() { FieldName = "model", ErrorDetail = "The model cannot be null." });
            }
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
