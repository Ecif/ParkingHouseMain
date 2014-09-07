using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Abstract;
using DAO.Concrete;
using Ninject;

namespace ParkingHouseMain.Infrastructure.Ninject
{
    public class NinjectControllerFactory
    {
        private IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }
        private void AddBindings()
        {
            _ninjectKernel.Bind<IClientsRepository>().To<ClientsRepository>();
            _ninjectKernel.Bind<IParkingHouse>().To<ParkingHouseRepository>();
        }

    }
}
