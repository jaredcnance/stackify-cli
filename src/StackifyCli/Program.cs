using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using StackifyCli.Client;
using StackifyCli.Terminal;
using StackifyCli.Deployments;
using StackifyCli.Options;

namespace StackifyCli
{
    class Program
    {
        private static bool _log;
        private static readonly CommandLineApplication _cli = new CommandLineApplication(throwOnUnexpectedArg: false);
        private static ServiceProvider _container;

        static Program()
        {
            var envLogValue = Environment.GetEnvironmentVariable("STACKIFY_CLI_LOG");
            _log = bool.TryParse(envLogValue, out bool shouldLog) ? shouldLog : false;

            _cli = new CommandLineApplication(throwOnUnexpectedArg: false);
            _cli.HelpOption("-? | -h | --help");

            ConfigureContainer();
            ConfigureDeploy();
        }

        private static void ConfigureContainer()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IApiClient, ApiClient>();
            services.AddSingleton<IDeploymentService, DeploymentService>();
            services.AddSingleton<IConsoleWriter, ConsoleWriter>();
            services.AddSingleton<DeploymentCommandProcessor>();
            _container = services.BuildServiceProvider();
        }

        static void ConfigureDeploy()
        {
            _cli.Command("deploy", deploy =>
            {
                deploy.HelpOption("-?| -h | --help");
                deploy.Description = "Work with deployments";
                deploy.Command("new", deployNew => ConfigureNew(deployNew));
                deploy.Command("get", deployGet => ConfigureGet(deployGet));
            });
        }

        static void ConfigureGet(CommandLineApplication cmd)
        {
            var options = DeployOptions.GetOptions(cmd);

            cmd.Invoke = () =>
            {
                var processor = _container.GetRequiredService<DeploymentCommandProcessor>();
                processor.GetAsync(options).Wait();
                return 1;
            };
        }

        static void ConfigureNew(CommandLineApplication cmd)
        {
            var options = DeployOptions.GetOptions(cmd);
            cmd.Invoke = () =>
            {
                var processor = _container.GetRequiredService<DeploymentCommandProcessor>();
                processor.New(options);
                return 1;
            };
        }

        static void Main(string[] args)
        {
            try
            {
                ConsoleSpiner.StartLoading();
                _cli.OnExecute(() => _cli.ShowHelp());
                _cli.Execute(args);
            }
            catch (Exception e)
            {
                ConsoleSpiner.StopLoading();
                Console.WriteLine(e.Message);
            }
            ConsoleSpiner.StopLoading();
        }
    }
}
