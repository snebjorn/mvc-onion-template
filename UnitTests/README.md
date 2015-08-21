## Coverage.bat

This bat files is used to generate coverage for your solution.
If this is the first time you're going to run it, then add the OpenCover.Profiler.dll, located in the packages folder, with regsvr32, remember admin rights. (ex. C:\ ... \packages\OpenCover.4.6.166\tools\x64>regsvr32 OpenCover.Profiler.dll)

Navigate and run coverage.bat from its current location in cmd like above, and it will generate and open a page that shows the coverage for all classes located in "Web.Controllers", so edit the filter if you want other namespaces included.
