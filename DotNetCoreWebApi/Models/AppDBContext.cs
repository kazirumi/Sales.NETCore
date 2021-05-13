using DotNetCoreWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Model
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {

        }
        public DbSet<SalesMain> SalesMains { get; set; }
        public DbSet<Item> items { get; set; }
        public DbSet<SalesSub> SalesSubs { get; set; }


    }
}
