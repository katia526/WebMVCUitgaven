using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVCUitgaven.Domain;

namespace WebMVCUitgaven.Database
{
    public class UitgaveDbContext : DbContext
    {
    
    
        public UitgaveDbContext(DbContextOptions<UitgaveDbContext> options) : base(options)
        {

        }
        public DbSet<Uitgave> Uitgaven { get; set; }
    }


}
