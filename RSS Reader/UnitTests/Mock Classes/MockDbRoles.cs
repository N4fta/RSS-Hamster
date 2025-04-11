using Data;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mock_Classes
{
    internal class MockDbRoles : IDbConnectionRoles
    {
        private List<string> rolesRepo = new();

        public MockDbRoles(List<string> rolesRepo)
        {
            this.rolesRepo = rolesRepo;
        }

        public DBResult InsertRole(string role)
        {
            if (rolesRepo.Contains(role))
            {
                return new DBResult(false, "Role already exists");
            }
            rolesRepo.Add(role);
            return new DBResult(true);
        }

        public List<string> LoadRoles()
        {
            return rolesRepo;
        }

        public DBResult DeleteRole(string role)
        {
            if (rolesRepo.Contains(role))
            {
                rolesRepo.Remove(role);
                return new DBResult(true);
            }
            return new DBResult(false, "Role not found");
        }
    }
}
