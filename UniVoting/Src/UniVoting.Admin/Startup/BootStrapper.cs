using Autofac;
using UniVoting.Admin.Administrators;

namespace UniVoting.Admin.Startup
{
	public class BootStrapper
	{
		public Autofac.IContainer BootStrap()
		{
			var builder =new ContainerBuilder();

			//register windows
		    builder.RegisterType<MainWindow>().AsSelf().InstancePerLifetimeScope();
            //builder.RegisterType<MainWindow>()
            //register pages
		    builder.RegisterType<AdminAddVotersPage>().AsSelf().InstancePerLifetimeScope();


            //register services


            return builder.Build();
		} 
	}
}