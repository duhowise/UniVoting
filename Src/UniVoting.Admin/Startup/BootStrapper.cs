

using System.Reflection;
using Autofac;
using Prism.Events;
using Univoting.Data;
using Univoting.Services;
using UniVoting.Admin.Administrators;

namespace UniVoting.Admin.Startup
{
	public class BootStrapper
	{
		public IContainer BootStrap()
		{
			var builder =new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().InstancePerLifetimeScope();
            builder.RegisterType<ElectionDbContext>().AsSelf().InstancePerLifetimeScope();
		    string reassembly = typeof(ElectionConfigurationService).GetTypeInfo().Assembly.GetName().Name;
            builder.RegisterType<VotingService>().As<IVotingService>().InstancePerLifetimeScope();
            builder.RegisterType<ElectionConfigurationService>().As<IElectionConfigurationService>().InstancePerLifetimeScope();
            builder.RegisterType<LiveViewService>().As<ILiveViewService>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.Load(reassembly)).AsImplementedInterfaces().InstancePerLifetimeScope();
           
            //register windows
            builder.RegisterType<MainWindow>().AsSelf().InstancePerLifetimeScope();
		    builder.RegisterType<AdminLoginWindow>().AsSelf().InstancePerLifetimeScope();
		    builder.RegisterType<AdminDispensePasswordWindow>().AsSelf().InstancePerLifetimeScope();
		    builder.RegisterType<PresidentLoginWindow>().AsSelf().InstancePerLifetimeScope();
            //builder.RegisterType<PresidentLoginWindow>()

            //register pages
            builder.RegisterType<AdminMenuPage>().AsSelf().InstancePerLifetimeScope();
		    builder.RegisterType<AdminAddVotersPage>().AsSelf().InstancePerLifetimeScope();
		    builder.RegisterType<AdminCreateAccountPage>().AsSelf().InstancePerLifetimeScope();
		    builder.RegisterType<AdminSetUpElectionPage>().AsSelf().InstancePerLifetimeScope();
		    builder.RegisterType<AdminSetUpCandidatesPage>().AsSelf().InstancePerLifetimeScope();
		    builder.RegisterType<AdminSetUpPositionPage>().AsSelf().InstancePerLifetimeScope();

            //register userControls
            builder.RegisterType<PositionControl>().AsSelf().InstancePerLifetimeScope();


            //register services
            //builder.RegisterModule<AplicationServiceModule>();
            return builder.Build();
		} 
	}
}