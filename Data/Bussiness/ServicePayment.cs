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
    /// <summary>
    /// Services for calculation and statistics.
    /// </summary>
    public class ServicePayment
    {
        private const double HourlyFee = 2.0;
        private const double ContractFee = 10.0;

        /// <summary>
        /// Creates check for each client and 
        /// adds up premium customer's sum and regular customer's sum.
        /// </summary>
        /// <param name="customer">Customer to take into account.</param>
        /// <param name="statistics">Statistics for sum and client count.</param>
        public void AddCheck(Client customer, Dictionary<string, double> statistics)
        {
            var sum = CreateCheck(customer);
            if(customer.HasContract)
                statistics["premiumSum"] += sum;
            else
                statistics["regularSum"] += sum;
            statistics["clientsCount"] ++;
        }
        /// <summary>
        /// Shows parking info and returns sum for parking.
        /// </summary>
        /// <param name="customer">Customer to take into account.</param>
        /// <param name="showLeaveText">Bool for showing text. 
        /// If simulation ends before everyone exits, then it's not 
        /// good to show everyone's cheks when they check out.</param>
        /// <returns>Returns Sum for parking.</returns>
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
        /// <summary>
        /// Returns sum of clients, who are still in parking house when simulation time ends.
        /// </summary>
        /// <param name="clients">List of clients, who are still in parking house after simulation end.</param>
        /// <param name="parkingSpaces">List of parking spaces</param>
        /// <returns>Returns sum for parking of those, who were left in parking house after simulation time runs out.</returns>
        public double CalculateRemainingSum(IList<Client> clients, List<ParkingSpace> parkingSpaces)
        {
            var sum = 0.0;
            foreach (var parkingSpace in parkingSpaces)
            {
                var customer = clients.FirstOrDefault(
                    client => client.Id == parkingSpace.ClientId && parkingSpace.ClientId != null);
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
