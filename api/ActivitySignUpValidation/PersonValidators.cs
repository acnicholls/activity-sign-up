﻿using ActivitySignUp.Models;
using ActivitySignUp.Models.Person;
using System.Net.Mail;
using ActivitySignUp.Validation.Interfaces;

namespace ActivitySignUp.Validation
{
    /// <summary>
    /// this validator class checks the person model for violations of data requirements
    /// </summary>
    public class PersonValidators : IPersonValidators
    {

        /// <summary>
        /// this method validates the person model on insert
        /// </summary>
        /// <param name="model">the model containing the details of the person</param>
        /// <returns>a ValidationResults model</returns>
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

        /// <summary>
        /// this method validates the email of the person to check
        /// </summary>
        /// <param name="email">the email of the person</param>
        /// <returns>a ValidationResults model</returns>
        public ValidationResults ValidateCheckEmail(string email)
        {
            var returnValue = new ValidationResults();
            if (email.Length > 120)
            {
                returnValue.ValidationErrors.Add(new ValidationError() { FieldName = "CheckEmail", ErrorDetail = "The email is too long." });
            }
            try
            {
                var address = new MailAddress(email);
            }
            catch
            {
                returnValue.ValidationErrors.Add(new ValidationError() { FieldName = "CheckEmail", ErrorDetail = "The email is not a valid email address." });
            }

            return returnValue;
        }

    }
}
