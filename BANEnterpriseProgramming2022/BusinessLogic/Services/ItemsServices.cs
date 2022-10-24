using BusinessLogic.ViewModels;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Services
{
    //note: when you need an instance of class to be used in all the methods, you may use 
    //      dependency injection to ask for an instance and .net core will automatically
    //      create an instance for you
    public class ItemsServices
    {
       private ItemsRepository itemRepository { get; set; }
        public ItemsServices(ItemsRepository ir) 
        {
            itemRepository = ir;

        }

        public void AddNewItem(string name, string description, double price, int categoryId,int stock =0, string path = "")
        {
           // ItemsRepository itemRepository = new ItemsRepository(new DataAccess.Context.ShoppingCartContext(new Microsoft.EntityFrameworkCore.DbContextOptions<DataAccess.Context.ShoppingCartContext>()));
            
            if (itemRepository.GetItems().Where(x=>x.Name == name).Count() > 0)
            {
                throw new Exception("Item already exist. Use a different name");
            }

            itemRepository.AddItem(new Domain.Models.Item()
            {
                CategoryId = categoryId,
                Description = description,
                Name = name,
                Price = price,
                Stock = stock,
                ImagePath = path
            }); 
        }

        public void DeleteItem(int id)
        {
           // ItemsRepository itemRepository = new ItemsRepository(new DataAccess.Context.ShoppingCartContext(new Microsoft.EntityFrameworkCore.DbContextOptions<DataAccess.Context.ShoppingCartContext>()));
            var item = itemRepository.GetItem(id);
            if (item != null) itemRepository.DeleteItem(item);
        }

        public IQueryable<ListItemsViewModel> GetItems()
        {
            /*
             * select i.Descripton,...... as .....
             * from Items
             * where
             * order by
             * */

            //to do

            var result = from i in itemRepository.GetItems()
                         select new ListItemsViewModel()
                         {
                             Description = i.Description,
                             ImagePath = i.ImagePath,
                             Name = i.Name,
                             Price = i.Price,
                             Rating = i.Rating,
                             Stock = i.Stock,
                             Id = i.Id,
                             Category = i.Category.Title
                         };

            return result;
        
        } 

        public ListItemsViewModel GetItem(int id)
        {

            return GetItems().SingleOrDefault(x => x.Id == id);
        }

         public IQueryable<ListItemsViewModel> Search(string keyword)
        {
            return GetItems().Where(x => x.Name.Contains(keyword)); //like %%
        }

        public IQueryable<ListItemsViewModel> Search(string keyword, double minPrice, double maxPrice)
        {
            //if using a List<...>
            //1st call GetItems() //opened a connection with the database, fetched all items from the databse and placed those in memory
            //2nd call Where(x=> x.Name.Contains (...)) //filtering would have happened inside the server's memory
            //3rd call Where(x=> x.Price >= minPrice && x.Price <= maxPrice); //further filtering would have happened inside the server's memory

            //if using IQueryable<...>
            //1st call GetItems() //a linq statement would have been prepared to get all items BUT not executed yet
            //2nd call Where(x=> x.Name.Contains (...)) //amending the prepared linq statement in (step 1)
            //3rd call Where(x=> x.Price >= minPrice && x.Price <= maxPrice); //amending the prepared linq statement in (step 1 & 2)
            //opening the connection with the database only happens when passing the Iqueryable into the View
            //IQueryable is more efficient because it opens the connection only once in the end, retrieving only a filtered list

            return Search(keyword).Where(x => x.Price >= minPrice && x.Price <= maxPrice);

            
        }

    }
}
