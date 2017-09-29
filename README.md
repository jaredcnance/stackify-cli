# stackify-cli

An unsupported CLI for Stackify functions.

## Downloading

To download the CLI binaries, go to the [releases](https://github.com/jaredcnance/stackify-cli/releases) page and get the latest version for your OS.


## Building from Sources

To build from source, you will need the [dotnet core SDK](https://www.microsoft.com/net/download/core) installed

```
git clone https://github.com/jaredcnance/stackify-cli.git
cd stackify-cli
dotnet build
cd ./src/StackifyCli
dotnet run {...arguments}
```

## Commands

### Deploy

This command set allows you to work with your Stackify deployments.

```
stackify deploy get
stackify deploy new
```

## Options

Options can either be set in the commands themselves or in environment variables prefixed by `STACKIFY_CLI_`.
For example, to set your API Key using an environment variable, you can do the following:

powershell:

```powershell
[Environment]::SetEnvironmentVariable("STACKIFY_CLI_APIKEY", "abc123", "User")
```

bash:

```bash
export STACKIFY_CLI_APIKEY="abc123"
```