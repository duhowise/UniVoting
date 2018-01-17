using Autofac;
using UniVoting.Data.Implementations;
using UniVoting.Data.Interfaces;

namespace UniVoting.Admin.StatUp
{
	public class BootStrapper
	{
		public IContainer BootStrap()
		{
			var builder =new ContainerBuilder();

			builder.RegisterType<ElectionService>().As<IService>();
			builder.RegisterType<CandidateRepository>().AsSelf();
			builder.RegisterType<ComissionerRepository>().AsSelf();
			builder.RegisterType<VoterRepository>().AsSelf();
			builder.RegisterType<PositionRepository>().AsSelf();

			return builder.Build();
		} 
	}
}