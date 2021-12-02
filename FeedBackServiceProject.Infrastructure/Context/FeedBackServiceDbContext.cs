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
            SeedData();
        }
        public DbSet<Feedback> Feedbacks{ get; set; }

        public void SeedData()
        {
            var feedback = new List<Feedback>()
            {
                new Feedback {Id = 1,Message=".net api core",CreatedBy="mohamed",Rating=7,Subject="science",CreatedDate=DateTime.Today },
                new Feedback {Id = 2,Message="Microservices",CreatedBy="ahmed",Rating=11,Subject="science",CreatedDate=DateTime.Today },
                new Feedback {Id = 3,Message="English",CreatedBy="samir",Rating=20,Subject="science",CreatedDate=DateTime.Today },
            };
            Feedbacks.AddRange(feedback);
            SaveChanges();
        }
    }
}
