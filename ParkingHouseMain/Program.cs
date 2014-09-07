using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAO.Abstract;
using DAO.Concrete;
using ParkingHouseMain.Infrastructure.Ninject;

namespace ParkingHouseMain
{
    class Program
    {
        static void Main(string[] args)
        {
            IClientsRepository clientsRepo = new ClientsRepository();
            IParkingHouse parkingHouseRepo = new ParkingHouseRepository(clientsRepo);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.Elapsed.Seconds < 10)
            {
                Thread.Sleep(500);
                var freeParkingSpotsCount = parkingHouseRepo.GetFreeParkingSpaceCount();
                var addTask = new Task(parkingHouseRepo.AddCustomer);                
                addTask.Start();
                addTask.Wait(1000);
                var removeTask = new Task(parkingHouseRepo.RemoveCustomer);
                if (parkingHouseRepo.GetSize() > freeParkingSpotsCount)
                {
                    removeTask.Start();
                    removeTask.Wait(4000);
                }

            }
            
            Console.WriteLine("Total sum - " + parkingHouseRepo.GetTotalSum());
            Console.WriteLine("Clients visited - " + parkingHouseRepo.GetTotalClientsCount());

            Console.ReadLine();
        }
    }
}
