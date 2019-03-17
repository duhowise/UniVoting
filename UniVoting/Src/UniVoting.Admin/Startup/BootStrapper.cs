using System.Reflection;
using Autofac;
using Univoting.Data;
using Univoting.Services;
using UniVoting.Admin.Administrators;
using UniVoting.Services;

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
            builder.RegisterType<MainWindow>().AsSelf().InstancePerDependency();
		    builder.RegisterType<AdminLoginWindow>().AsSelf().InstancePerDependency();
		    builder.RegisterType<AdminDispensePasswordWindow>().AsSelf().InstancePerDependency();
		    builder.RegisterType<PresidentLoginWindow>().AsSelf().InstancePerDependency();
		    builder.RegisterType<EcChairmanLoginWindow>().AsSelf().InstancePerDependency();
            //builder.RegisterType<MainWindow>()
            //register pages
		    builder.RegisterType<AdminAddVotersPage>().AsSelf().InstancePerDependency();
		    builder.RegisterType<AdminCreateAccountPage>().AsSelf().InstancePerDependency();
		    builder.RegisterType<AdminSetUpElectionPage>().AsSelf().InstancePerDependency();
		    builder.RegisterType<AdminSetUpCandidatesPage>().AsSelf().InstancePerDependency();
		    builder.RegisterType<AdminSetUpPositionPage>().AsSelf().InstancePerDependency();

            return builder.Build();
		} 
	}
}