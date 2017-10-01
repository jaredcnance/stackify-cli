# stackify-cli
![](https://travis-ci.org/jaredcnance/stackify-cli.svg)
![](https://ci.appveyor.com/api/projects/status/0xitfocaqtb8w3os?svg=true)

An unsupported cross-platform CLI for Stackify functions.

## Downloading

To download the CLI binaries, go to the [releases](https://github.com/jaredcnance/stackify-cli/releases) page and get the latest version for your OS.


## Building from Source

To build from source, you will need the [dotnet core SDK](https://www.microsoft.com/net/download/core) installed

```
git clone https://github.com/jaredcnance/stackify-cli.git
cd stackify-cli
dotnet build
cd ./src/StackifyCli
dotnet run {...arguments}
```

## Commands

Below are the supported commands. 
* All commands include a `-h | --help` option for getting more info. 
* All commands that return data, will return it in JSON format by default, or YAML if the `-p | --pretty` flag is provided

### Deploy

Provides a set of commands for working with your Stackify deployments.

```
stackify deploy get
stackify deploy start
stackify deploy cancel
stackify deploy complete
```

## Options

Options can either be set in the commands themselves or in environment variables prefixed by `STACKIFY_CLI_`.
For example, to set your API Key using an environment variable, you can do the following:

cmd:
```cmd
setx STACKIFY_CLI_APIKEY "abc123"
```

powershell:

```powershell
[Environment]::SetEnvironmentVariable("STACKIFY_CLI_APIKEY", "abc123", "User")
```

bash:

```bash
export STACKIFY_CLI_APIKEY="abc123"
```