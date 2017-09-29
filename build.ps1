dotnet restore
dotnet build

$win = "win7-x64"
$osx = "osx.10.11-x64"
$ubuntu = "ubuntu.16.04-x64"
$runtimes = $win,$osx,$ubuntu

Foreach ($runtime in $runtimes)

{
    Write-Information "Publishing $($runtime)"
    dotnet publish -c Release -r $runtime
    
    $zipName = "stackify-cli-$($env:APPVEYOR_REPO_TAG_NAME)-$($runtime).tar"
    $src = ".\src\StackifyCli\bin\Release\netcoreapp2.0\$($runtime)\publish"
    Write-Information "Zipping $($runtime) to $($zipName)"

    #C:\bin\7z.exe a $zipName $src
    7z a $zipName $src
}
