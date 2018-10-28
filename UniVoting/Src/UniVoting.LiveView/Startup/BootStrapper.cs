using Autofac;
using UniVoting.Core;
using UniVoting.Services;


namespace UniVoting.LiveView.Startup
{
	public class BootStrapper
	{
		public Autofac.IContainer BootStrap()
		{
			var builder =new ContainerBuilder();

			//register windows
		    builder.RegisterType<MainWindow>().AsSelf().InstancePerLifetimeScope();

            //register pages
		    builder.RegisterType<TileControlLarge>().AsSelf().InstancePerLifetimeScope();
		    builder.RegisterType<TileControlSmall>().AsSelf().InstancePerLifetimeScope();

            //register services
		    builder.RegisterType<LiveViewService>().As<ILiveViewService>().InstancePerLifetimeScope();
		    builder.RegisterType<ElectionDbContext>().AsSelf().InstancePerLifetimeScope();


            return builder.Build();
		} 
	}
}