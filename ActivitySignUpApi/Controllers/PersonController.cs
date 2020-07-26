using ActivitySignUp.Models.Person;
using ActivitySignUp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ActivitySignUp.Api.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private IPersonService _service;
        private ILogger<PersonController> _logger;

        public PersonController(
            IPersonService service,
            ILogger<PersonController> logger
            )
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// this method will sign a new user up for an activity
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="activityId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> InsertPersonAsync(
            string firstName,
            string lastName,
            string email,
            int activityId
            )
        {
            var person = new PersonInsertModel()
            {
                PersonActivityId = activityId,
                PersonEmail = email,
                PersonFirstName = firstName,
                PersonLastName = lastName
            };

            try
            {
                var srvResult = await _service.InsertPersonAsync(person);
                if (srvResult.IsSuccessful)
                {
                    return new OkObjectResult(srvResult.Payload);
                }
                if(srvResult.IsException)
                {
                    return Problem(detail: srvResult.Exception.Exception, title: srvResult.Exception.Location);
                }
                return new BadRequestObjectResult(srvResult.Errors);
            }
            catch(Exception x)
            {
                _logger.LogError(x, "InsertPersonAsync");
                return Problem(detail: x.StackTrace, title: x.Message);
            }
        }
    }
}
