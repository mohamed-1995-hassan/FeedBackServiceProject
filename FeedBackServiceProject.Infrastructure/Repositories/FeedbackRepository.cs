using AutoMapper;
using FeedBackServiceProject.Core.Interfaces.Repositories;
using FeedBackServiceProject.Core.Models;
using FeedBackServiceProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackServiceProject.Infrastructure.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly FeedBackServiceDbContext _feedBackServiceDbContext;
        private readonly IMapper _mapper;
        public FeedbackRepository(FeedBackServiceDbContext feedBackServiceDbContext, IMapper mapper)
        {
            _feedBackServiceDbContext = feedBackServiceDbContext??throw new ArgumentNullException(nameof(feedBackServiceDbContext));
            _mapper = mapper?? throw new ArgumentNullException(nameof(mapper));
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
            var dbFeedbacks = await _feedBackServiceDbContext.Feedbacks.ToListAsync();
            return _mapper.Map<IEnumerable<Feedback>>(dbFeedbacks);
        }

        public async Task<Feedback> GetFeedbackById()
        {
            throw new NotImplementedException();
        }
    }
}
