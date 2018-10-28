using System.Reflection;
using Autofac;
using UniVoting.Admin.Administrators;
using UniVoting.Services;
using UniVoting.Services.Startup;

namespace UniVoting.Admin.Startup
{
	public class BootStrapper
	{
		public IContainer BootStrap()
		{
			var builder =new ContainerBuilder();
		    string reassembly = typeof(ElectionConfigurationService).GetTypeInfo().Assembly.GetName().Name;
            //register windows
            builder.RegisterType<MainWindow>().AsSelf().InstancePerLifetimeScope();
		    builder.RegisterType<AdminLoginWindow>().AsSelf().InstancePerLifetimeScope();
            //builder.RegisterType<MainWindow>()
            //register pages
		    builder.RegisterType<AdminAddVotersPage>().AsSelf().InstancePerLifetimeScope();


            //register services
		    builder.RegisterModule<AplicationServiceModule>();
            return builder.Build();
		} 
	}
}