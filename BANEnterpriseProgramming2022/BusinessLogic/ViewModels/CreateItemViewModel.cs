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
       
        public int Stock { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

  
        public int CategoryId { get; set; }



        public string ImagePath { get; set; }

        public IQueryable<CategoryViewModel> Categories { get; set; }


    }
}
