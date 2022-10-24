using DataAccess.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class CategoriesRepository
    {
        private ShoppingCartContext context;
        public CategoriesRepository(ShoppingCartContext _context)
        {
            context = _context;
        }

        public IQueryable<Category> GetCategories()
        {
            return context.Categories;
        }
    }
}
