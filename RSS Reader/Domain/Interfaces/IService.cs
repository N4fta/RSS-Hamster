using Data;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    // How my Service classes will look
    public interface IService
    {
        public DBResult Add(object obj);

        // Role and Category Service won't have update as their tables act as more flexible Enums
        public DBResult Update(object obj);

        public DBResult Delete(object obj);

        // Will differ based on filter options per class
        public List<object> Get();


        // These should be private but the Interface complains if they are
        // Role and Category are excluded
        public object ConvertToDTO(object obj);

        public List<object> ConvertToDTO(List<object> objList);

        public object ConvertToDomainClass(object obj);

        public List<object> ConvertToDomainClass(List<object> objList);

    }
}
