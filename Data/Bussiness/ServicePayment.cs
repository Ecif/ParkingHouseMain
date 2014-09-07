using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataSource.DataClients;
using Entities.Main.Clients;
using Entities.Main.ParkingHouse;

namespace Data.Bussiness
{
    public class ServicePayment
    {
        private const double HourlyFee = 2.0;
        private const double ContractFee = 10.0;

        // 1s = 1h
        public void AddCheck(Client customer, Dictionary<string, double> statistics)
        {
            var sum = CreateCheck(customer);
            if(customer.HasContract)
                statistics["premiumSum"] += sum;
            else
                statistics["regularSum"] += sum;
            statistics["clientsCount"] ++;
        }

        private double CreateCheck(Client customer, bool showLeaveText = true)
        {
            
            var timeParking = (TimeSpan)(customer.DepartureTime - customer.EntryTime);
            var secondsParking = timeParking.Seconds;
            var timeSpentParking = Math.Round((decimal) secondsParking/10, 2);
            var fee = customer.HasContract ? ContractFee : (HourlyFee*
                              (double) (Math.Ceiling(timeSpentParking) != 0 ? Math.Ceiling(timeSpentParking) : 1));
            if (showLeaveText)
            {
                Console.WriteLine(" ----LEFT----");
                Console.WriteLine("Customer left. ({0}) ", customer.HasContract ? "Premium" : "Regular");
                Console.WriteLine("         Time spent parking - {0} hour(s). Fee - {1}.", timeSpentParking,
                     fee); 
            }                
            return fee;
        }

        public double CalculateRemainingSum(List<Client> clients, List<ParkingSpace> parkingSpaces)
        {
            var sum = 0.0;
            foreach (var parkingSpace in parkingSpaces)
            {
                var customer = clients.FirstOrDefault(
                    client => client.ClientId == parkingSpace.ClientId && parkingSpace.ClientId != null);
                if (customer != null)
                {
                    customer.DepartureTime = DateTime.Now;
                    sum += CreateCheck(customer, false);
                }
                    
            }
            return sum;
        }
    }
}
