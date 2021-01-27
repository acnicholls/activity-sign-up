using ActivitySignUp.Models.Activity;
using ActivitySignUp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ActivitySignUp.Api.Controllers
{
    /// <summary>
    /// activity controller class
    /// </summary>
    [Route("api/activity")]
    [ApiController]
    public class ActivityController : ControllerBase
    {

        private readonly IActivityService _service;
        private readonly ILogger<ActivityController> _logger;

        /// <summary>
        /// activity controller ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="service"></param>
        public ActivityController(
            ILogger<ActivityController> logger,
            IActivityService service
            )
        {
            _service = service;
            _logger = logger;
        }


        /// <summary>
        /// this method will insert a new activity
        /// </summary>
        /// <param name="activity">model containing the values to insert</param>
        /// <returns>the newly inserted Id</returns>
        [HttpPost("new")]
        public async Task<ActionResult> InsertActivityAsync(ActivityInsertModel activity)
        {
            try
            {
                var srvResult = await _service.InsertActivityAsync(activity);
                if (srvResult.IsSuccessful)
                {
                    return new OkObjectResult(srvResult.Payload);
                }
                if (srvResult.IsException)
                {
                    return Problem(detail: srvResult.Exception.Exception, title: srvResult.Exception.Location);
                }
                return new BadRequestObjectResult(srvResult.Errors);
            }
            catch (Exception x)
            {
                _logger.LogError(x, "InsertActivityAsync");
                return Problem(detail: x.StackTrace, title: x.Message);
            }
        }

        /// <summary>
        /// this method will get the list of all activities
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetActivityListAsync()
        {
            try
            {
                var srvResult = await _service.GetActivityListAsync();
                if (srvResult.IsSuccessful)
                {
                    return new OkObjectResult(srvResult.Payload);
                }
                if (srvResult.IsException)
                {
                    return Problem(detail: srvResult.Exception.Exception, title: srvResult.Exception.Location);
                }
                return new BadRequestObjectResult(srvResult.Errors);
            }
            catch (Exception x)
            {
                _logger.LogError(x, "GetActivityListAsync");
                return Problem(detail: x.StackTrace, title: x.Message);
            }
        }

        /// <summary>
        /// this method will get an activity's data for a non-signed up user
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        [HttpGet("{activityId}")]
        public async Task<ActionResult> GetInitialActivityViewAsync(int activityId)
        {
            try
            {
                var srvResult = await _service.GetInitialActivityViewAsync(activityId);
                if (srvResult.IsSuccessful)
                {
                    return new OkObjectResult(srvResult.Payload);
                }
                if (srvResult.IsException)
                {
                    return Problem(detail: srvResult.Exception.Exception, title: srvResult.Exception.Location);
                }
                return new BadRequestObjectResult(srvResult.Errors);
            }
            catch (Exception x)
            {
                _logger.LogError(x, "GetInitialActivityViewAsync");
                return Problem(detail: x.StackTrace, title: x.Message);
            }
        }

        /// <summary>
        /// this method will get the activity's data for a signed in user
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        [HttpGet("{activityId}/signed-up")]
        public async Task<ActionResult> GetSignedUpActivityViewAsync(int activityId)
        {
            try
            {
                var srvResult = await _service.GetSignedUpActivityViewAsync(activityId);
                if (srvResult.IsSuccessful)
                {
                    return new OkObjectResult(srvResult.Payload);
                }
                if (srvResult.IsException)
                {
                    return Problem(detail: srvResult.Exception.Exception, title: srvResult.Exception.Location);
                }
                return new BadRequestObjectResult(srvResult.Errors);
            }
            catch (Exception x)
            {
                _logger.LogError(x, "GetSignedUpActivityViewAsync");
                return Problem(detail: x.StackTrace, title: x.Message);
            }
        }

    }
}
