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
    public class RoleServiceTests
    {
        [TestMethod]
        public void AddRole_Test()
        {
            // Repo
            var roleRepo = new List<string>()
            {
                "Reader",
                "Administrator",
                "Reporter"
            };
            var mockDbRoles = new MockDbRoles(roleRepo);

            // Others
            var role = "Test";

            var roleService = new RoleService(mockDbRoles);

            // Act
            var result = roleService.Add(role);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(roleRepo.Count == 4);
        }

        [TestMethod]
        public void DeleteRole_Test()
        {
            // Repo
            var roleRepo = new List<string>()
            {
                "Reader",
                "Administrator",
                "Reporter"
            };
            var mockDbRoles = new MockDbRoles(roleRepo);

            // Others
            var role = "Reporter";

            var roleService = new RoleService(mockDbRoles);

            // Act
            var result = roleService.Delete(role);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(roleRepo.Count == 2);
        }

        [TestMethod]
        public void GetAllRoles_Test()
        {
            // Repo
            var roleRepo = new List<string>()
            {
                "Reader",
                "Administrator",
                "Reporter"
            };
            var mockDbRoles = new MockDbRoles(roleRepo);

            // Others
            var roleService = new RoleService(mockDbRoles);

            // Act
            var result = roleService.GetAll();

            // Assert
            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result.SequenceEqual(roleRepo));

        }
    }
}
