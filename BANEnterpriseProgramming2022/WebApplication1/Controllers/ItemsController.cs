using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class ItemsController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] //because when you submit a form you are triggering a Post
        public IActionResult Create(CreateItemViewModel data) //<<<< same class used by the template to create the View
        {
            try
            {
                //call the ItemsService AddItem method
                ViewBag.Message = "Item added successfully";
            }
            catch(Exception ex)
            {
                //log the error

                ViewBag.Error = "Item was not added successfully. Please check inputs";
            }
            return View();
        }
    }
}
