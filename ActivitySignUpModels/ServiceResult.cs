using System.Collections.Generic;
using System.Linq;
using System;

namespace ActivitySignUp.Models
{
    public class ServiceResult<T>
    {
        public ServiceResult(T value)
        {
            Payload = value;
            Errors = new List<ValidationError>();
        }

        public ServiceResult(List<ValidationError> errorList)
        {
            Errors = errorList;
        }

        public ServiceResult(ValidationError error)
        {
            Errors = new List<ValidationError>() { error };
        }

        public ServiceResult(ServiceError serviceError)
        {
            Exception = serviceError;
            Errors = new List<ValidationError>();
        }

        public bool IsSuccessful => !Errors.Any() && Exception==null;
        public T Payload { get; set; }
        public List<ValidationError> Errors { get; set; }
        public bool IsException => !Errors.Any();
        public ServiceError Exception { get; set; }
    }
}
