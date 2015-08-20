echo off
cls
..\packages\OpenCover.4.6.166\tools\opencover.console -output:coverage.xml -target:"..\packages\xunit.runner.console.2.0.0\tools\xunit.console.exe" -targetargs:"bin\Debug\UnitTests.dll -noshadow" -filter:"+[*]Web.Controllers*"

echo "Hvis intet bliver comitted, så registrer OpenCover.Profiler.dll med regsvr32"

..\packages\ReportGenerator.2.1.8.0\tools\ReportGenerator coverage.xml .\coverage

start .\coverage\index.htm