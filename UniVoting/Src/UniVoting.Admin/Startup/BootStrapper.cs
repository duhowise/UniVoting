using System.Reflection;
using Autofac;
using UniVoting.Admin.Administrators;
using UniVoting.Core;
using UniVoting.Services;
using UniVoting.Services.Startup;

namespace UniVoting.Admin.Startup
{
	public class BootStrapper
	{
		public IContainer BootStrap()
		{
			var builder =new ContainerBuilder();
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
            //builder.RegisterType<MainWindow>()
            //register pages
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