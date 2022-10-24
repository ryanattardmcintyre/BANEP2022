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
        private CategoriesServices categoriesService;

        //constructor injection is applied so that any creation of instances that one needs (aka the service class)
        //in the class to be consumed (aka client class) in a central place (for better efficiency)
        //that happens to be the class startup.cs (aka the injector class)
        public ItemsController(ItemsServices _itemsService, CategoriesServices _categoriesService)
        {
            itemsService = _itemsService;
            categoriesService = _categoriesService;
        }


        [HttpGet] //the get method, opens a View with blank controls
        public IActionResult Create()
        {
            //need to get a list of categories and pass that list to the View
            var listOfCategories = categoriesService.GetCategories();
            //need to pass listOfCategories into the View
            //Approach 1: 
            //amend CreateItemViewModel in such a way that it holds even a list of Categories
            CreateItemViewModel myModel = new CreateItemViewModel();
            myModel.Categories = listOfCategories;
            //Approach 2:
           // ViewBag.Categories = listOfCategories;

            return View(myModel);
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

            //var listOfCategories = categoriesService.GetCategories();
            //CreateItemViewModel myModel = new CreateItemViewModel();
            //myModel.Categories = listOfCategories;
            //return View(myModel);
            return RedirectToAction("Create");

        }


        public IActionResult List() //query type
        {
            var list = itemsService.GetItems();
            return View(list); //note: to present data on the page...you need to pass the object within the View
        }

        public IActionResult Details(int id)
        {
            var item = itemsService.GetItem(id);
            if (item == null)
            {
                //redirect the user not to the Details page but to List page
                ViewBag.Error = "Item doesn't exist";
                var list = itemsService.GetItems();
                return View("List", list); //Details View
            }
            else  return View(item); //Details View
        }

        public IActionResult Search(string keyword)
        {
            var list = itemsService.Search(keyword);
            return View("List", list);
        }
    }
}
