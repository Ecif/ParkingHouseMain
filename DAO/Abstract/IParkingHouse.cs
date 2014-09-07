using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataSource.DataParkingHouse;

namespace DAO.Abstract
{
    public interface IParkingHouse
    {
        void AddCustomer();

        void RemoveCustomer();
        int GetFreeParkingSpaceCount();
        int GetSize();
        double GetTotalSum();
        int GetTotalClientsCount();
    }
}
