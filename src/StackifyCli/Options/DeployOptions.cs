using McMaster.Extensions.CommandLineUtils;

namespace StackifyCli.Options
{
    public class DeployOptions : ApiOptions
    {
        public static DeployOptions GetOptions(CommandLineApplication cmd)
        {
            cmd.HelpOption("-? | -h | --help");

            return new DeployOptions(cmd)
            {
                _appOption = cmd.Option("-a | --app", "Application name [string] (STACKIFY_CLI_APP)", CommandOptionType.SingleValue),
                _environmentOption = cmd.Option("-e | --env", "Environment name [string] (STACKIFY_CLI_ENV)", CommandOptionType.SingleValue),
                _versionOption = cmd.Option("-v | --version", "Version [string] (STACKIFY_CLI_DEPLOY_VERSION)", CommandOptionType.SingleValue),
                _nameOption = cmd.Option("-n | --name", "Name [string] (STACKIFY_CLI_DEPLOY_NAME)", CommandOptionType.SingleValue),
                _uriOption = cmd.Option("-u | --uri", "URI [string] (STACKIFY_CLI_DEPLOY_URI)", CommandOptionType.SingleValue),
                _branchOption = cmd.Option("-b | --branch", "Branch [string] (STACKIFY_CLI_DEPLOY_BRANCH)", CommandOptionType.SingleValue),
                _commitOption = cmd.Option("-c | --commit", "Commit [string] (STACKIFY_CLI_DEPLOY_COMMIT)", CommandOptionType.SingleValue),

                _prettyOption = cmd.Option("-p | --pretty", "Pretty [flag]", CommandOptionType.NoValue)
            };
        }

        private DeployOptions(CommandLineApplication cmd) : base(cmd) { }

        private CommandOption _appOption { get; set; }
        private CommandOption _environmentOption { get; set; }
        private CommandOption _versionOption { get; set; }
        private CommandOption _nameOption { get; set; }
        private CommandOption _uriOption { get; set; }
        private CommandOption _branchOption { get; set; }
        private CommandOption _commitOption { get; set; }
        private CommandOption _prettyOption { get; set; }


        public string App => _appOption.HasValue() ? _appOption.Value() : Config.Get("APP");
        public string Environment => _environmentOption.HasValue() ? _environmentOption.Value() : Config.Get("ENVIRONMENT");
        public string Version => _versionOption.HasValue() ? _versionOption.Value() : Config.Get("DEPLOY_VERSION");
        public string Name => _nameOption.HasValue() ? _nameOption.Value() : Config.Get("DEPLOY_NAME");
        public string Uri => _uriOption.HasValue() ? _uriOption.Value() : Config.Get("DEPLOY_URI");
        public string Branch => _branchOption.HasValue() ? _branchOption.Value() : Config.Get("DEPLOY_BRANCH");
        public string Commit => _commitOption.HasValue() ? _commitOption.Value() : Config.Get("DEPLOY_COMMIT");
        public bool Pretty => _prettyOption.HasValue();
    }
}