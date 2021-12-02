using FeedBackServiceProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackServiceProject.Core.Interfaces.Services
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllFeedbacks();
        Task<Feedback> GetFeedbackById();
        Task<bool> CreateFeedback(Feedback feedback);
        Task<bool> DeleteFeedback(int id);
    }
}
