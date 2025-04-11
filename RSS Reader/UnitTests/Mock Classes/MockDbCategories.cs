using CodeHollow.FeedReader;
using Data;
using Data.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mock_Classes
{
    internal class MockDbCategories : IDbConnectionCategories
    {
        private List<string> categoriesRepo = new();

        public MockDbCategories(List<string> categoriesRepo)
        {
            this.categoriesRepo = categoriesRepo;
        }

        public DBResult InsertCategory(string category)
        {
            if (categoriesRepo.Contains(category))
            {
                return new DBResult(false, "Category already exists");
            }
            categoriesRepo.Add(category);
            return new DBResult(true);
        }

        public List<string> LoadCategories()
        {
            return categoriesRepo;
        }

        public DBResult DeleteCategory(string category)
        {
            if (categoriesRepo.Contains(category))
            {
                categoriesRepo.Remove(category);
                return new DBResult(true);
            }
            return new DBResult(false, "Category not found");
        }
    }
}
