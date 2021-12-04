using FeedBackServiceProject.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackServiceProject.Infrastructure.Context
{
   public  class FeedBackServiceDbContext : DbContext
    {
        public FeedBackServiceDbContext(DbContextOptions<FeedBackServiceDbContext>options):base(options)
        {
        }
        public virtual DbSet<Feedback> Feedbacks{ get; set; }

    }
}
