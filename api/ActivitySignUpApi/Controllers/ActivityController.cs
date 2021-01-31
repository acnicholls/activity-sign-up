using ActivitySignUp.Models.Activity;
using ActivitySignUp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.IO;
using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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
        /// saves uploaded file to local folder
        /// </summary>
        /// <returns></returns>
        [HttpPost("image")]
        public async Task<ActionResult> UploadActivityImage()
        {

            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files[0];
                if(file.Length > 2097152)
                {
                    return Problem(detail: "The file is larger than 2 mb.", title: "ActivityImage");
                }
                if(!file.FileName.EndsWith("jpg", ignoreCase: true, CultureInfo.InvariantCulture))
                {
                    return Problem(detail: "Only JPG files are accepted.", title: "ActivityImage");
                }
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var ImagePath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return Ok(new { ImagePath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

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
                _logger.LogInformation("InsertActivityAsync executing...");
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
                _logger.LogInformation("GetActivityListAsync executing...");
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
                _logger.LogInformation("GetInitialActivityViewAsync executing...");
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
                _logger.LogInformation("GetSignedUpActivityViewAsync executing...");
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
