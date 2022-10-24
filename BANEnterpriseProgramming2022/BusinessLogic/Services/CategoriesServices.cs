using BusinessLogic.ViewModels;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Services
{
    public class CategoriesServices
    {
 
        private CategoriesRepository categoryRepository { get; set; }
        public CategoriesServices(CategoriesRepository cr)
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
