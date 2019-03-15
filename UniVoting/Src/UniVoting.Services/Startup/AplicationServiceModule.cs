using System.Reflection;
using Autofac;
using Univoting.Data;
using Univoting.Services;
using UniVoting.Data;
using Module = Autofac.Module;


namespace UniVoting.Services.Startup
{
    public class AplicationServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            string reassembly = typeof(ElectionConfigurationService).GetTypeInfo().Assembly.GetName().Name;


            builder.RegisterType<DialogService>().As<IDialogService>().InstancePerLifetimeScope();
            builder.RegisterType<VotingService>().As<IVotingService>().InstancePerLifetimeScope();
            builder.RegisterType<ElectionConfigurationService>().As<IElectionConfigurationService>().InstancePerLifetimeScope();
            builder.RegisterType<LiveViewService>().As<ILiveViewService>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.Load(reassembly)).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ElectionDbContext>().AsSelf().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}