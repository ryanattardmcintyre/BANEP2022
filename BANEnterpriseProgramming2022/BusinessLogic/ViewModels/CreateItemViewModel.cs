using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BusinessLogic.ViewModels
{
    //ViewModels are to be used to pass on data from/to the View & Controllers
    //ViewModels usually contain a selection of the properties we have in our database, therefore
    // we will be hiding any properties containing sensitive data
    public class CreateItemViewModel
    {
       public int Id { get; set; }
        public int Stock { get; set; }

        [StringLength(100, ErrorMessage ="Name of item cannot be greater than 100 characters")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="Name cannot be left blank")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Price cannot be left blank")]
        [Range(1, 50000, ErrorMessage ="Price has got to be between 1 and 50000 euros")]
        public double Price { get; set; }

        

        [CategoryIdValidation()]
        public int CategoryId { get; set; }



        public string ImagePath { get; set; }

        public IQueryable<CategoryViewModel> Categories { get; set; }


    }
}
