using Autofac;
using UniVoting.Services.Startup;

namespace UniVoting.Client.Startup
{
	public class BootStrapper
	{
		public IContainer BootStrap()
		{
			var builder =new ContainerBuilder();

            //register windows
            //builder.RegisterType<MainWindow>()
            //register pages


            //register services

		    builder.RegisterModule<AplicationServiceModule>();

            return builder.Build();
		} 
	}
}