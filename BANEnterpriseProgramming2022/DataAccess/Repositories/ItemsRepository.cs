using DataAccess.Context;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class ItemsRepository
    {
        private ShoppingCartContext context;
        public ItemsRepository(ShoppingCartContext _context) 
        {
            context = _context;
        }
        public void AddItem(Item i)
        {
            context.Items.Add(i);
            context.SaveChanges(); //without this nothing will be saved into the database
        }

        //List > it returns a list of items ready made with all the info downloaded in memory
        //IQueryable > it returns a list of items pointers that point to items within the db context.
                      //adv is: that any search or filter that are added to the returned list are executed in the db

        public IQueryable<Item> GetItems()
        {
            return context.Items;
        }
        

        /*public void Test()
        {   //list vs iqueryable
            GetItems().Where(x => x.Stock < 2).OrderBy(x => x.Price);
            //list: 1000 items in memory, it filters the 1000 from the memory

            //iqueryable: no items are in memory, it will keep adding filters, then when it is about to display them on 
            //            the page, it then executes the sql in the database and it returns only a few products
        }
        */
        public Item GetItem(int id)
        {
            //i is a variable
            //=> lambda expression 
            //i.Id == id the condition
            return GetItems().SingleOrDefault(i => i.Id == id);

            /*
             *   foreach(Item i in GetItems())
             *   {
             *      if(i.Id == id)
             *              return i;
             *   }
             *   return null;
             */
        }
        
        public void DeleteItem(Item i)
        {
            context.Items.Remove(i);
            context.SaveChanges();
        }
        
        public void EditItem(Item updatedItem)
        {
            var originalItem = GetItem(updatedItem.Id);

            originalItem.Name = updatedItem.Name;
            originalItem.Description = updatedItem.Description;
            originalItem.CategoryId = updatedItem.CategoryId;
            originalItem.ImagePath = updatedItem.ImagePath;
            originalItem.Price = updatedItem.Price;
            originalItem.Stock = updatedItem.Stock;

            context.SaveChanges();
        }

    }
}
