using BusinessLogic.Services;
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
        private ItemsServices itemsService;

        //constructor injection is applied so that any creation of instances that one needs (aka the service class)
        //in the class to be consumed (aka client class) in a central place (for better efficiency)
        //that happens to be the class startup.cs (aka the injector class)
        public ItemsController(ItemsServices _itemsService)
        {
            itemsService = _itemsService;
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] //because when you submit a form you are triggering a Post
        public IActionResult Create(CreateItemViewModel data) //<<<< same class used by the template to create the View
        {
            //to do Show ViewBag in page

            try
            {
                itemsService.AddNewItem(data.Name, data.Description, data.Price, data.CategoryId, data.Stock, data.ImagePath);
                //call the ItemsService AddItem method

                //ViewBag is a dynamic object -it is constructed in realtime (in other words on-the-fly)
                
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
