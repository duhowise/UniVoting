using Autofac;
using UniVoting.Services.Startup;


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
		    builder.RegisterModule<AplicationServiceModule>();


            return builder.Build();
		} 
	}
}