using BusinessLogic.ViewModels;
using DataAccess.Repositories;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Services
{
    public class CategoriesServices
    {
 
        private ICategoriesRepository categoryRepository { get; set; }
        public CategoriesServices(ICategoriesRepository cr)
        {
            categoryRepository = cr;

        }

        public IQueryable<CategoryViewModel> GetCategories()
        {
            //this is going to be flattened in 1 line of code
            //by using AutoMapper

            var result = from c in categoryRepository.GetCategories()
                         select new CategoryViewModel()
                         {
                             Title = c.Title,
                             Id = c.Id

                         };
            return result;
        }
    }
}
