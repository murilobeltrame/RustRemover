dotnet stryker --solution  "$(Get-Location)\RustRemover.sln"
$latestFolder = Get-ChildItem -Directory .\StrykerOutput\ | Sort-Object CreationTime -desc | Select-Object -f 1
Invoke-Expression "$(Get-Location)\StrykerOutput\$(Write-Output $latestFolder)\reports\mutation-report.html"