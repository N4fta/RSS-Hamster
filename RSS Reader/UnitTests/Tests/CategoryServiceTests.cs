using Data;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Mock_Classes;

namespace UnitTests.Tests
{
    [TestClass]
    public class CategoryServiceTests
    {
        [TestMethod]
        public void AddCategory_Test()
        {
            // Repo
            var categoryRepo = new List<string>()
            {
                "Sports",
                "News",
                "Entertainment"
            };
            var mockDbCategories = new MockDbCategories(categoryRepo);

            // Others
            var category = "Test";

            var categoryService = new CategoryService(mockDbCategories);

            // Act
            var result = categoryService.Add(category);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(categoryRepo.Count == 4);
        }

        [TestMethod]
        public void DeleteCategory_Test()
        {
            // Repo
            var categoryRepo = new List<string>()
            {
                "Sports",
                "News",
                "Entertainment"
            };
            var mockDbCategories = new MockDbCategories(categoryRepo);

            // Others
            var category = "Sports";

            var categoryService = new CategoryService(mockDbCategories);

            // Act
            var result = categoryService.Delete(category);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(categoryRepo.Count == 2);
        }

        [TestMethod]
        public void GetAllCategories_Test()
        {
            // Repo
            var categoryRepo = new List<string>()
            {
                "Sports",
                "News",
                "Entertainment"
            };
            var mockDbCategories = new MockDbCategories(categoryRepo);

            // Others
            var categoryService = new CategoryService(mockDbCategories);

            // Act
            var result = categoryService.GetAll();

            // Assert
            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result.SequenceEqual(categoryRepo));

        }
    }
}
