#!/usr/bin/env pwsh

param (
    [string]$Version = "0.0.1-dev",
    [string]$ApiKey,
    [string]$Source = "https://api.nuget.org/v3/index.json"
)

# Set working directory to the script location
$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $scriptPath

# Build and pack the project
Write-Host "Building and packing Katasec.DStream.Providers version $Version..."
dotnet pack ./src/Katasec.DStream.Providers/Katasec.DStream.Providers.csproj -c Release /p:Version=$Version

# Check if ApiKey is provided
if ([string]::IsNullOrEmpty($ApiKey)) {
    Write-Host "NuGet package created at ./src/Katasec.DStream.Providers/bin/Release/Katasec.DStream.Providers.$Version.nupkg"
    Write-Host "To publish, run this script with -ApiKey parameter"
} else {
    # Push the package to NuGet
    Write-Host "Publishing Katasec.DStream.Providers version $Version to $Source..."
    dotnet nuget push ./src/Katasec.DStream.Providers/bin/Release/Katasec.DStream.Providers.$Version.nupkg --api-key $ApiKey --source $Source
}
