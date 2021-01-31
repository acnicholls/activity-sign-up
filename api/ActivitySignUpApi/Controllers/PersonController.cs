using ActivitySignUp.Models.Person;
using ActivitySignUp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ActivitySignUp.Api.Controllers
{
    /// <summary>
    /// this controller handles methods for participants
    /// </summary>
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private IPersonService _service;
        private ILogger<PersonController> _logger;

        /// <summary>
        /// basic ctor
        /// </summary>
        /// <param name="service">the person service</param>
        /// <param name="logger">the logger</param>
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
        /// <param name="person">the model containing the values of the new record</param>
        /// <returns>the newly inserted Id</returns>
        [HttpPost]
        public async Task<ActionResult> InsertPersonAsync(PersonInsertModel person)
        {
            try
            {
                _logger.LogInformation("InsertPersonAsync executing...");
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
