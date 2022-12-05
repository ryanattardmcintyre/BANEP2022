using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    //this reads/writes data to/from a file
    public class CategoriesFileRepository : ICategoriesRepository
    {
        //FileInfo its a built-in class represeting a File
        private FileInfo context;
        public CategoriesFileRepository(FileInfo _context)
        {
            context = _context;
        }

        public IQueryable<Category> GetCategories()
        {
            string line = "";
            Category myCategory;
            List<Category> categories = new List<Category>();
            using (StreamReader sr = context.OpenText())
            {
                while(sr.Peek() != -1) //while there is more to read from the file
                {
                    line = sr.ReadLine();
                    myCategory = new Category()
                    {
                        Id = Convert.ToInt32(line.Split(';')[0]),
                        Title = line.Split(';')[1]
                    };
                    categories.Add(myCategory);
                }
            }

            return categories.AsQueryable();
        }
    }
}
