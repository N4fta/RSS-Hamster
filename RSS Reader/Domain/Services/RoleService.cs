using Data.Interfaces;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DatabaseConnections;

namespace Domain.Services
{
    public class RoleService
    {
        private IDbConnectionRoles _connectionRoles;

        public RoleService()
        {
            _connectionRoles = new DbConnectionRoles();
        }
        public RoleService(IDbConnectionRoles dbConnectionRoles)
        {
            _connectionRoles = dbConnectionRoles;
        }

        public DBResult Add(string role)
        {
            return _connectionRoles.InsertRole(role);
        }

        public DBResult Delete(string role)
        {
            return _connectionRoles.DeleteRole(role);
        }

        public List<string> GetAll()
        {
            return _connectionRoles.LoadRoles();
        }
    }
}
