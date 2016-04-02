using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using ParkingSystem.Core.ReservationRules;
using ParkingSystem.Core.ReservationRules.Definitions;
using ParkingSystem.Core.Services;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.Repository.Core;
using ParkingSystem.WebUI.Identity;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.Core.Time;
using ParkingSystem.Core.Time.Convertors;
using ParkingSystem.Core.Time.Utils;

namespace ParkingSystem.WebUI
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ParkingSystemDbContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerLifetimeScope();
            
            builder.Register(c => new UserStore<ApplicationUser>(c.Resolve<ParkingSystemDbContext>()))
                .AsImplementedInterfaces().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication);

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<ParkingSpotService>().As<IParkingSpotService>().InstancePerLifetimeScope();
            builder.RegisterType<ReservationService>().As<IReservationService>().InstancePerLifetimeScope();
            builder.RegisterType<CalendarService>().As<ICalendarService>().InstancePerLifetimeScope();

            builder.RegisterType<DateToWeekOfYearConvertor>().As<IDateToWeekOfYearConvertor>().InstancePerLifetimeScope();
            builder.RegisterType<WeekOfYearToDateConvertor>().As<IWeekOfYearToDateConvertor>().InstancePerLifetimeScope();
            builder.RegisterType<CurrentTimeForCentralEuropeTimeZone>().As<ICurrentTime>().InstancePerLifetimeScope();
            builder.RegisterType<DatesOfBusinessDaysCounter>().As<IDatesOfBusinessDaysCounter>().InstancePerLifetimeScope();
            builder.RegisterType<WeekOfYearIterator>().As<IWeekOfYearIterator>().InstancePerLifetimeScope();
            builder.RegisterType<DayOfWeekUtils>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<WeekendDayUtils>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<ReservationRulesValidator>().As<IReservationRulesValidator>().InstancePerLifetimeScope();
            builder.RegisterType<ReservationRulesForParkingSpot>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterModule(new AutofacWebTypesModule());
            builder.RegisterFilterProvider();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}