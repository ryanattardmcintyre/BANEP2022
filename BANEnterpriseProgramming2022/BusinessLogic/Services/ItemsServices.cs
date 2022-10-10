﻿using DataAccess.Repositories;
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

        public void AddNewItem(string name, string description, double price, int categoryId,int stock =0)
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
                Stock = stock
            }); 
        }

        public void DeleteItem(int id)
        {
           // ItemsRepository itemRepository = new ItemsRepository(new DataAccess.Context.ShoppingCartContext(new Microsoft.EntityFrameworkCore.DbContextOptions<DataAccess.Context.ShoppingCartContext>()));
            var item = itemRepository.GetItem(id);
            if (item != null) itemRepository.DeleteItem(item);
        }

        /*public IQueryable<Item> GetItems()
        { }*/
    }
}
