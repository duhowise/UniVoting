using Autofac;

namespace UniVoting.Client.Startup
{
	public class BootStrapper
	{
		public Autofac.IContainer BootStrap()
		{
			var builder =new ContainerBuilder();

			//register windows
            //builder.RegisterType<MainWindow>()
            //register pages


            //register services


			return builder.Build();
		} 
	}
}