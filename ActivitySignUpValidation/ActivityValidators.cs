using ActivitySignUp.Models;
using ActivitySignUp.Models.Activity;
using ActivitySignUp.Validation.Interfaces;

namespace ActivitySignUp.Validation
{
    /// <summary>
    /// this validator class checks the activity model for violations of data requirements
    /// </summary>
    public class ActivityValidators : IActivityValidators
    {
        /// <summary>
        /// this method validates the activity model on insert
        /// </summary>
        /// <param name="model">the model containing the details of the activity</param>
        /// <returns>a ValidationResults model</returns>
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
