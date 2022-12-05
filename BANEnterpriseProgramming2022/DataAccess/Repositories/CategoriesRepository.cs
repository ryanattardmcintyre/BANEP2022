using DataAccess.Context;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    //this reads/writes data to /from db
    public class CategoriesRepository: ICategoriesRepository
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
