using Data;
using Data.DatabaseConnections;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CategoryService
    {
        private IDbConnectionCategories _connectionCategories;

        public CategoryService()
        {
            _connectionCategories = new DbConnectionCategories();
        }
        public CategoryService(IDbConnectionCategories dbConnectionCategories)
        {
            _connectionCategories = dbConnectionCategories;
        }

        public DBResult Add(string category)
        {
            return _connectionCategories.InsertCategory(category);
        }

        public DBResult Delete(string category)
        {
            return _connectionCategories.DeleteCategory(category);
        }

        public List<string> GetAll()
        {
            return _connectionCategories.LoadCategories().Select(cat=>cat.Trim()).ToList();
        }
    }
}
