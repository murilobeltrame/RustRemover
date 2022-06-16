dotnet test --no-build /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
~\.nuget\packages\reportgenerator\5.1.9\tools\net47\ReportGenerator.exe -reports:"$(Get-Location)\RustRemover.Api.Tests\coverage.opencover.xml" -targetdir:"$(Get-Location)\RustRemover.Api.Tests\report"
Invoke-Expression "$(Get-Location)\RustRemover.Api.Tests\report\index.html"