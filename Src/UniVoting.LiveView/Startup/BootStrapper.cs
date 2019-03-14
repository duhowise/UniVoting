using System.Reflection;
using Autofac;
using Univoting.Data;
using Univoting.Services;


namespace UniVoting.LiveView.Startup
{
	public class BootStrapper
	{
		public IContainer BootStrap()
		{
			var builder =new ContainerBuilder();

			//register windows
		    builder.RegisterType<MainWindow>().AsSelf().InstancePerLifetimeScope();

            //register pages
		    builder.RegisterType<TileControlLarge>().AsSelf().InstancePerLifetimeScope();
		    builder.RegisterType<TileControlSmall>().AsSelf().InstancePerLifetimeScope();

            //register services
            builder.RegisterType<ElectionDbContext>().AsSelf().InstancePerLifetimeScope();
            string reassembly = typeof(ElectionConfigurationService).GetTypeInfo().Assembly.GetName().Name;
            builder.RegisterType<VotingService>().As<IVotingService>().InstancePerLifetimeScope();
            builder.RegisterType<ElectionConfigurationService>().As<IElectionConfigurationService>().InstancePerLifetimeScope();
            builder.RegisterType<LiveViewService>().As<ILiveViewService>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.Load(reassembly)).AsImplementedInterfaces().InstancePerLifetimeScope();
            //builder.RegisterModule<AplicationServiceModule>();


            return builder.Build();
		} 
	}
}