using FeedBackServiceProject.Core.Interfaces.Services;
using FeedBackServiceProject.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeedBackServiceProject.Api.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        public readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService??throw new ArgumentNullException(nameof(feedbackService));
        }

        [HttpGet]
        [Route("GetFeedBacks")]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetAllFeedbacks()
        {
           var response = await _feedbackService.GetAllFeedbacks();
            if(response==null)
            {
                return NoContent();
            }
            return Ok(response);
        }
    }
}
