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
        private static readonly CommandLineApplication _cli = new CommandLineApplication(throwOnUnexpectedArg: false);
        private static ServiceProvider _container;

        static Program()
        {
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
                deploy.HelpOption("-? | -h | --help");
                deploy.Description = "Work with deployments";
                deploy.OnExecute(() => deploy.ShowHelp());
                deploy.Command("start", start => ConfigureStart(start));
                deploy.Command("cancel", cancel => ConfigureCancel(cancel));
                deploy.Command("complete", complete => ConfigureComplete(complete));
                deploy.Command("get", get => ConfigureGet(get));
            });
        }

        static void ConfigureGet(CommandLineApplication cmd)
        {
            var options = DeployOptions.GetOptions(cmd);
            cmd.Description = "Get deployments";
            cmd.Invoke = () =>
            {
                var processor = _container.GetRequiredService<DeploymentCommandProcessor>();
                processor.GetAsync(options).Wait();
                return 1;
            };
        }

        static void ConfigureComplete(CommandLineApplication cmd)
        {
            var options = DeployOptions.GetOptions(cmd);
            cmd.Description = "Complete a pending deployment request or create a new, completed deployment";
            cmd.Invoke = () =>
            {
                var processor = _container.GetRequiredService<DeploymentCommandProcessor>();
                processor.CompleteAsync(options).Wait();
                return 1;
            };
        }

        static void ConfigureStart(CommandLineApplication cmd)
        {
            var options = DeployOptions.GetOptions(cmd);
            cmd.Description = "Start a deployment in a pending state. Deployment must be completed or cancelled.";
            cmd.Invoke = () =>
            {
                var processor = _container.GetRequiredService<DeploymentCommandProcessor>();
                processor.StartAsync(options).Wait();
                return 1;
            };
        }

        static void ConfigureCancel(CommandLineApplication cmd)
        {
            var options = DeployOptions.GetOptions(cmd);
            cmd.Description = "Cancel a pending deployment";
            cmd.Invoke = () =>
            {
                var processor = _container.GetRequiredService<DeploymentCommandProcessor>();
                processor.CancelAsync(options).Wait();
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
