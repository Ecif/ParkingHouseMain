using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataSource.DataClients;
using Data.DataSource.DataVehicles;

namespace DAO.Abstract
{
    public interface IClientsRepository
    {
        void CreateClientsList();
        Clients GetClients();        
    }
}
