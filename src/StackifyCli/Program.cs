using System;
using McMaster.Extensions.CommandLineUtils;
using StackifyCli.Options;

namespace StackifyCli
{
    class Program
    {
        private static bool _log;
        private static readonly CommandLineApplication _cli = new CommandLineApplication(throwOnUnexpectedArg: false);

        static Program()
        {
            var envLogValue = Environment.GetEnvironmentVariable("STACKIFY_CLI_LOG");
            _log = bool.TryParse(envLogValue, out bool shouldLog) ? shouldLog : false;

            _cli = new CommandLineApplication(throwOnUnexpectedArg: false);
            _cli.HelpOption("-? | -h | --help");

            ConfigureDeploy();
        }

        static void ConfigureDeploy()
        {
            _cli.Command("deploy", deploy =>
            {
                deploy.HelpOption("-?");
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
                Console.WriteLine($"GET {options.App}");
                return 1;
            };
        }

        static void ConfigureNew(CommandLineApplication cmd)
        {
            var options = DeployOptions.GetOptions(cmd);
            cmd.Invoke = () =>
            {
                Console.WriteLine($"NEW {options.ApiKey}");
                return 1;
            };
        }

        static void Main(string[] args)
        {
            _cli.OnExecute(() => _cli.ShowHelp());
            _cli.Execute(args);
        }
    }
}
