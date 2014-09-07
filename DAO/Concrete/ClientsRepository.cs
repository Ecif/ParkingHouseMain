using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Abstract;
using Data.DataSource.DataClients;
using Data.DataSource.DataVehicles;
using Entities.Main.Clients;
using Entities.Main.Vehicles;

namespace DAO.Concrete
{
    /// <summary>
    /// Client repository.
    /// </summary>
    public class ClientsRepository : IClientsRepository
    {
        private readonly Clients _clients;
        private List<Car> _cars;

        public ClientsRepository()
        {
            _clients = new Clients();            
            CreateClientsList();
        }
        /// <summary>
        /// Creates default client list.
        /// </summary>
        public void CreateClientsList()
        {
            for (var i = 0; i < 400; i++)
            {
                _clients.ClientList.Add(
                    new Client
                    {
                        Id = i,
                        EntryTime = DateTime.Now,
                        HasContract = i % 10 == 0 ,
                        DepartureTime = null
                    });
            }
            CreateCarsList();
            BindCarsToClients();
        }        
        /// <summary>
        /// Creates default car list.
        /// </summary>
        private void CreateCarsList()
        {
            _cars = Cars.CarsList;
            for (var i = 1; i <= 3; i++)
                {
                    _cars.Add(
                        new Car
                        {
                            Id = i,
                            CarLength = i == 3 ? 5 : 4,
                            CarWidth = 2
                        });
                }            
        }
        /// <summary>
        /// Bind a car for each client.
        /// </summary>
        private void BindCarsToClients()
        {
            
            if (_cars != null && _clients != null)
            {
                foreach (var client in _clients.ClientList)
                {
                    if (client.Id == 100)
                        client.Vehicle = new Car {Id = 2};
                    else if (client.Id == 200 || client.Id == 300)
                        client.Vehicle = new Car {Id = 3};
                    else
                        client.Vehicle = new Car { Id = 1 }; 
                }
            }
        }
        public Clients GetClients()
        {
            return _clients;
        }
    }
}
