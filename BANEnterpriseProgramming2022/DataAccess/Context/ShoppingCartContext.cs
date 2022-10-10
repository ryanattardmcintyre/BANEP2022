using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Context
{
    //Note: ShoppingCartContext is an abstraction of the database
     public class ShoppingCartContext : IdentityDbContext
    {
        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
           : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
    } 
}
