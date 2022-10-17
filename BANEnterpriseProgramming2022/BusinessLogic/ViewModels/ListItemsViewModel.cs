using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.ViewModels
{
    public class ListItemsViewModel
    {
        public int Id { get; set; }
        public int Stock { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public double Price { get; set; }

        public string Category { get; set; }

        public string ImagePath { get; set; }

 
    }
}
