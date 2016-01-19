[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Presentation.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Presentation.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Presentation.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Infrastructure.DataAccess;
    using Core.DomainServices;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataProtection;
    using Microsoft.AspNet.Identity.Owin;
    using RazorEngine.Templating;
    using System.Net.Mail;
    using Mail;
    using Core.DomainModel;
    using System.Data.Entity;
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ApplicationContext>().ToSelf().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            // Binding trick to avoid having to bind all usages of IGenericRepository. This binds them all!
            kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));

            // Identity
            kernel.Bind(typeof(IUserStore<>)).To(typeof(UserStore<>)).InRequestScope().WithConstructorArgument("context", kernel.Get<ApplicationContext>());
            kernel.Bind<IAuthenticationManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            kernel.Bind<IDataProtectionProvider>()
                .To<DpapiDataProtectionProvider>()
                .InRequestScope()
                .WithConstructorArgument("ApplicationName");

            kernel.Bind<ApplicationUserManager>().ToSelf().InRequestScope()
                .WithPropertyValue("UserTokenProvider",
                    new DataProtectorTokenProvider<ApplicationUser>(
                        kernel.Get<IDataProtectionProvider>().Create("EmailConfirmation")));

            // Mail
            //kernel.Bind<IRazorEngineService>().ToMethod(m => RazorEngineService.Create());
            //kernel.Bind<SmtpClient>().ToSelf();
            //kernel.Bind<IMailHandler>().To<MailHandler>();
            //kernel.Bind<IIdentityMessageService>().To<EmailService>();
        }
    }
}
