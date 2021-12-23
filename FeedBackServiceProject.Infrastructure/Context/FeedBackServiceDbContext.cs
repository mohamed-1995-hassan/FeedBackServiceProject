using FeedBackServiceProject.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedBackServiceProject.Infrastructure.Context
{
   public  class FeedBackServiceDbContext : IdentityDbContext<ApplicationUser>
    {
        public FeedBackServiceDbContext(DbContextOptions<FeedBackServiceDbContext>options):base(options)
        {
        }
        public virtual DbSet<Feedback> Feedbacks{ get; set; }

    }
}
