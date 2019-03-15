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
            builder.RegisterType<MainWindow>().SingleInstance();
            builder.RegisterType<ClientsLoginWindow>().InstancePerDependency();
            builder.RegisterType<CandidateControl>().InstancePerDependency();
            builder.RegisterType<ClientVoteCompletedPage>().InstancePerDependency();
            builder.RegisterType<ConfirmDialogControl>().InstancePerDependency();
            builder.RegisterType<ClientVotingPage>().InstancePerDependency();
          //register pages


            //register services

            builder.RegisterModule<AplicationServiceModule>();

            return builder.Build();
		} 
	}
}