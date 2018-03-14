using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TableTennis.Domain.Models;
using TableTennis.Domain.Repository.Abstract;
using TableTennis.Domain.Repository.Concrete;

namespace TableTennis.API.NinjectHelper
{
    public class NinjectBindings
    {
        public static void AddBindings(IKernel kernel)
        {
            kernel.Bind<IUserRepo>().To<UserRepo>();
            kernel.Bind<IUserTeamRepo>().To<UserTeamRepo>();
        }
    }
}