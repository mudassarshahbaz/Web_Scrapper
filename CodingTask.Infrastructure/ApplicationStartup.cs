namespace CodingTask.Infrastructure
{
    using Autofac;

    using CodingTask.Infrastructure.Logging;

    using Serilog;

    public class ApplicationStartup
    {
        private static void RegisterLoggingModule(ILogger logger)
        {
            ContainerBuilder container = new ContainerBuilder();

            container.RegisterModule(new LoggingModule(logger));
        }
    }
}