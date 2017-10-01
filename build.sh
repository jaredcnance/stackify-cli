dotnet restore
dotnet build
dotnet publish -c Release -r osx.10.11-x64
dotnet publish -c Release -r win7-x64
dotnet publish -c Release -r ubuntu.16.04-x64