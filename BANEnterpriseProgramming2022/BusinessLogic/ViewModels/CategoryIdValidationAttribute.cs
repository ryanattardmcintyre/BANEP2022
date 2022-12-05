using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;
using Domain.Interfaces;

namespace BusinessLogic.ViewModels
{
    public class CategoryIdValidationAttribute: ValidationAttribute
    {
        private ICategoriesRepository categoryRepo { get; set; }
        public CategoryIdValidationAttribute( ) {
           
        }
    
        public string GetErrorMessage() =>
            $"Category is not valid";

        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            categoryRepo = (ICategoriesRepository)validationContext
                         .GetService(typeof(ICategoriesRepository));

            var list = categoryRepo.GetCategories();
            int min = list.Min(x => x.Id);
            int max = list.Max(x => x.Id);

            int userInput = (int)value;

            if (userInput < min || userInput > max)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}


 