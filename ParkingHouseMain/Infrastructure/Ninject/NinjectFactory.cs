using DAO.Abstract;
using DAO.Concrete;
using Ninject;

namespace ParkingHouseMain.Infrastructure.Ninject
{
    /// <summary>
    /// Dependency injection.
    /// </summary>
    public class NinjectControllerFactory
    {
        private readonly IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }
        /// <summary>
        /// Binds ClientsRepository and ParkingHouseRepository to IClientsRepository and IparkingHouse respectevly.
        /// </summary>
        private void AddBindings()
        {
            _ninjectKernel.Bind<IClientsRepository>().To<ClientsRepository>();
            _ninjectKernel.Bind<IParkingHouse>().To<ParkingHouseRepository>();
        }

    }
}
