using FeedBackServiceProject.Core.Interfaces.Services;
using FeedBackServiceProject.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeedBackServiceProject.Api.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //[Route("api/" + ApiConstants.ServiceName + "/v{api-version:apiVersion}/[controller]")]
    //[ApiVersion("1.0")]
    //[ApiController]
    public class FeedbackController : ControllerBase
    {
        
        public readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService??throw new ArgumentNullException(nameof(feedbackService));
        }

        [HttpGet]
        [Route("GetFeedBacks")]
        [HttpGet]
        public ActionResult<IEnumerable<Feedback>> GetAllFeedbacks()
        {
            return Ok(_feedbackService.GetAllFeedbacks());
        }

        [HttpGet("{id}")]
        public ActionResult<Feedback> GetFeedback(int id)
        {
            var emp = _feedbackService.GetFeedbackById(id).Result;

            if (emp == null)
            {
                return NotFound();
            }
            return emp;
        }

        [HttpPost]
        public ActionResult<Feedback> PostFeedback(Feedback feedback)
        {
            _feedbackService.CreateFeedback(feedback);

            return CreatedAtAction("GetEmployee", new { id = feedback.Subject });
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public IActionResult DeleteFeedback(int id)
        {
            var employee = _feedbackService.DeleteFeedback(id);
            if (employee == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
