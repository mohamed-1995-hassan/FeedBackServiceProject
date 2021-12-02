using FeedBackServiceProject.Core.Interfaces.Repositories;
using FeedBackServiceProject.Core.Interfaces.Services;
using FeedBackServiceProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackServiceProject.Core.Services
{
    public class FeedbackService : IFeedbackService
    {
        public readonly IFeedbackRepository _feedbackRepository;
        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository??throw new ArgumentNullException(nameof(feedbackRepository));
        }
        public async Task<bool> CreateFeedback(Feedback feedback)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteFeedback(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacks()
        {
            return await _feedbackRepository.GetAllFeedbacks();
        }

        public async Task<Feedback> GetFeedbackById()
        {
            throw new NotImplementedException();
        }
    }
}
