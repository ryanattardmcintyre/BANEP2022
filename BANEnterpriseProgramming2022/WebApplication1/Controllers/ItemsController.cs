using BusinessLogic.Services;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private IWebHostEnvironment hostService;
        //constructor injection is applied so that any creation of instances that one needs (aka the service class)
        //in the class to be consumed (aka client class) in a central place (for better efficiency)
        //that happens to be the class startup.cs (aka the injector class)
        public ItemsController(ItemsServices _itemsService, CategoriesServices _categoriesService, IWebHostEnvironment _hostService)
        {
            hostService = _hostService;
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
        public IActionResult Create(CreateItemViewModel data, IFormFile file) //<<<< same class used by the template to create the View
        {
            //to do Show ViewBag in page

            try
            {
                //-------------------------- Upload of image --------------------------


                //1. check whether image has been successfully received

                if(file != null)
                {

                    //2. generate a unique filename to replace the original filename e.g. Guid
                    string uniqueFilename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);

                    //3. identify where to save the uploaded image & you need to get the absolute Path
                    //e.g. absolutePath C:\Users\attar\source\repos\BANEP2022\BANEnterpriseProgramming2022\WebApplication1\wwwroot\Images\
                    //you may get the absolutePath from a built-in Framework service (class) called IWebHostEnvironment
                    string absolutePath = hostService.WebRootPath + @"\Images\" + uniqueFilename;

                    //4. save the file into the identified absolutePath

                    using (var destinationFile = System.IO.File.Create(absolutePath)) //creates a file where the data will be transferred to
                    {
                        file.CopyTo(destinationFile); //actually copies the data from the user uploading file to the destination file
                    } //closes all open files

                    //5. set the newly filename + Images folder path into the object that's going to be saved into db
                    data.ImagePath = "/Images/" + uniqueFilename;

                }


                //------------------------------saving in db---------------------------------

                itemsService.AddNewItem(data.Name, data.Description, data.Price, data.CategoryId, data.Stock, data.ImagePath);
                //call the ItemsService AddItem method

                //ViewBag is a dynamic object -it is constructed in realtime (in other words on-the-fly)
                
                 ViewBag.Message = "Item added successfully";
                //Alternatively:
                //TempData["Message"] = "Item added successfully";
            }
            catch(Exception ex)
            {
                //log the error

                ViewBag.Error = "Item was not added successfully. Please check inputs";
            }

           var listOfCategories = categoriesService.GetCategories();
            CreateItemViewModel myModel = new CreateItemViewModel();
            myModel.Categories = listOfCategories;
            return View(myModel); 
            // return RedirectToAction("Create"); //ViewBag doesn't surive redirections

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

        public IActionResult Delete(int id)
        {
            itemsService.DeleteItem(id);
            return RedirectToAction("List");

        }
    }
}
