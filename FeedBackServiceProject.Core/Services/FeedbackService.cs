using FeedBackServiceProject.Core.Interfaces.Repositories;
using FeedBackServiceProject.Core.Interfaces.Services;
using FeedBackServiceProject.Core.Models;
using Microsoft.Extensions.Logging;
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
        public readonly ILogger<FeedbackService> _logger;

        public FeedbackService(IFeedbackRepository feedbackRepository, ILogger<FeedbackService> logger)
        {
            _feedbackRepository = feedbackRepository ?? throw new ArgumentNullException(nameof(feedbackRepository));
            _logger = logger;
        }
        public async Task<bool> CreateFeedback(Feedback feedback)
        {
            try
            {
                return await _feedbackRepository.CreateFeedback(feedback);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteFeedback(int id)
        {
            try
            {
                return await _feedbackRepository.DeleteFeedback(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacks()
        {
            try
            {
                return await _feedbackRepository.GetAllFeedbacks();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Feedback> GetFeedbackById(int id)
        {
            try
            {
                Feedback feedback = await _feedbackRepository.GetFeedbackById(id);
                return feedback;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
