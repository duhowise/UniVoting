// ***********************************************************************
// Assembly         : UniVoting.Client
// Author           : Duho
// Created          : 10-28-2018
//
// Last Modified By : Duho
// Last Modified On : 03-09-2019
// ***********************************************************************
// <copyright file="BootStrapper.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Reflection;
using Autofac;
using Univoting.Data;
using Univoting.Services;
using UniVoting.Admin.Administrators;
using UniVoting.Services;

namespace UniVoting.Client.Startup
{
    /// <summary>
    /// Class BootStrapper.
    /// </summary>
    public class BootStrapper
    {
        /// <summary>
        /// Boots the strap.
        /// </summary>
        /// <returns>Autofac.IContainer.</returns>
        public Autofac.IContainer BootStrap()
        {

            var builder = new ContainerBuilder();

            //builder.RegisterType<EventAggregator>().As<IEventAggregator>().InstancePerLifetimeScope();
            builder.RegisterType<ElectionDbContext>().AsSelf().InstancePerLifetimeScope();
            string reassembly = typeof(ElectionConfigurationService).GetTypeInfo().Assembly.GetName().Name;
            builder.RegisterType<VotingService>().As<IVotingService>().InstancePerLifetimeScope();
            builder.RegisterType<ElectionConfigurationService>().As<IElectionConfigurationService>().InstancePerLifetimeScope();
            builder.RegisterType<LiveViewService>().As<ILiveViewService>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(Assembly.Load(reassembly)).AsImplementedInterfaces().InstancePerLifetimeScope();

            ////register windows
            builder.RegisterType<ClientsLoginWindow>().AsSelf().InstancePerDependency();
            //builder.RegisterType<AdminLoginWindow>().AsSelf().InstancePerLifetimeScope();
            //builder.RegisterType<AdminDispensePasswordWindow>().AsSelf().InstancePerLifetimeScope();
            //builder.RegisterType<PresidentLoginWindow>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<MainWindow>().SingleInstance();

            //register pages
            builder.RegisterType<AdminMenuPage>().AsSelf().InstancePerDependency();
            builder.RegisterType<AdminAddVotersPage>().AsSelf().InstancePerDependency();
            builder.RegisterType<AdminCreateAccountPage>().AsSelf().InstancePerDependency();
            builder.RegisterType<AdminSetUpElectionPage>().AsSelf().InstancePerDependency();
            builder.RegisterType<AdminSetUpCandidatesPage>().AsSelf().InstancePerDependency();
            builder.RegisterType<AdminSetUpPositionPage>().AsSelf().InstancePerDependency();

            //register services
            //builder.RegisterModule<AplicationServiceModule>();
            return builder.Build();
        }
    }
}