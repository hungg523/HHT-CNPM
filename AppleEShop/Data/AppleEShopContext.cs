using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppleEShop.Models;

namespace AppleEShop.Data
{
    public class AppleEShopContext : DbContext
    {
        public AppleEShopContext(DbContextOptions<AppleEShopContext> options)
            : base(options)
        {
        }

        public DbSet<AppleEShop.Models.Category> Category { get; set; } = default!;

        public DbSet<AppleEShop.Models.Contact>? Contact { get; set; }

        public DbSet<AppleEShop.Models.Product>? Product { get; set; }

        public DbSet<AppleEShop.Models.User>? User { get; set; }

        public DbSet<AppleEShop.Models.Comment>? Comment { get; set; }
        public DbSet<AppleEShop.Models.Provinces>? Provinces { get; set; }
        public DbSet<AppleEShop.Models.Districts>? Districts { get; set; }
        public DbSet<AppleEShop.Models.Wards>? Wards { get; set; }
        public DbSet<AppleEShop.Models.Payment>? Delivery { get; set; }
        public DbSet<AppleEShop.Models.Order>? Order { get; set; }
        public DbSet<AppleEShop.Models.NewsLetterSubscription>? NewsLetterSubscription { get; set; }
    }
}
