using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDbConnectionRoles
    {
        DBResult InsertRole(string role);

        List<string> LoadRoles();

        // So far there is no need for this function since the table is so simple
        //DBResult UpdateRole(string role);

        DBResult DeleteRole(string role);
    }
}
