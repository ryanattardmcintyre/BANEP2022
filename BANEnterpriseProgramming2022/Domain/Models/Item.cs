using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    //Classes within the Domain Models, will be used to model/shape the database within a
    //code-first approach
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Stock { get; set; }
        
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public double Price { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string ImagePath { get; set; }

//        public string Supplier { get; set; }
    }
}
