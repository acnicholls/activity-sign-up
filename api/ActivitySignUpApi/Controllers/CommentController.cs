using ActivitySignUp.Models.Comment;
using ActivitySignUp.Repositories.Interfaces;
using ActivitySignUp.Services.Interfaces;
using ActivitySignUp.Validation.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ActivitySignUp.Api.Controllers
{
    /// <summary>
    /// this controller handles all methods for comments
    /// </summary>
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly ICommentService _service;
        private readonly ILogger<CommentController> _logger;

        /// <summary>
        /// basic ctor
        /// </summary>
        /// <param name="logger">the logger</param>
        /// <param name="service">the comment service</param>
        public CommentController(
            ILogger<CommentController> logger,
            ICommentService service
            )
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// this method will insert a new comment
        /// </summary>
        /// <param name="comment">the model containing the values of the new record</param>
        /// <returns>teh newly inserted Id</returns>
        [HttpPost]
        public async Task<ActionResult> InsertCommentAsync(CommentInsertModel comment)
        {
            try
            {
                var srvResult = await _service.InsertCommentAsync(comment);
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
                _logger.LogError(x, "InsertCommentAsync");
                return Problem(detail: x.StackTrace, title: x.Message);
            }
        }
    }
}
