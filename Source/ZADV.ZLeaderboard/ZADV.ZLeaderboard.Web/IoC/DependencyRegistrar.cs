using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Zadv.ZLeaderboard.Domain.IRepositories;
using ZADV.ZLeaderboard.Data;
using ZADV.ZLeaderboard.Domain.IRepositories;

namespace ZADV.ZLeaderboard.Web.IoC
{
    public static class DependencyRegistrar
    {
        public static IUnityContainer Container { get; private set; }

        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            //GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IDataContext, DataContext>(new PerRequestLifetimeManager(), new InjectionConstructor());
            container.RegisterType<IEventRepository, EventRepository>();
            container.RegisterType<IParticipantRepository, ParticipantRepository>();
            container.RegisterType<IVoterRepository, VoterRepository>();
        }
    }
}