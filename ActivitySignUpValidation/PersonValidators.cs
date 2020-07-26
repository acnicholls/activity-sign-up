using ActivitySignUp.Models;
using ActivitySignUp.Models.Person;
using System.Net.Mail;

namespace ActivitySignUp.Validation
{
    public class PersonValidators
    {

        public ValidationResults ValidateInsertModel(PersonInsertModel model)
        {
            var returnValue = new ValidationResults();
            if (model.PersonFirstName.Length > 50)
            {
                returnValue.ValidationErrors.Add(new ValidationError() { FieldName = "PersonFirstName", ErrorDetail = "The first name is too long." });
            }
            if (model.PersonLastName.Length > 50)
            {
                returnValue.ValidationErrors.Add(new ValidationError() { FieldName = "PersonFirstName", ErrorDetail = "The last name is too long." });
            }
            if (model.PersonEmail.Length > 120)
            {
                returnValue.ValidationErrors.Add(new ValidationError() { FieldName = "PersonEmail", ErrorDetail = "The email is too long." });
            }
            try
            {
                var address = new MailAddress(model.PersonEmail);
            }
            catch
            {
                returnValue.ValidationErrors.Add(new ValidationError() { FieldName = "PersonEmail", ErrorDetail = "The email is not a valid email address." });
            }

            return returnValue;

        }


    }
}
