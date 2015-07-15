To get 32-bit Crystal Reports to install on a 64-bit machine, you have to [edit the bootstrapper product.xml file](http://stackoverflow.com/a/16484268/212978).

The location of the bootstrapper file to modify depends on the [version of Visual Studio](https://en.wikipedia.org/wiki/Microsoft_Windows_SDK#Versions) you are using. For Visual Studio 2013, look in folder `C:\Program Files (x86)\Microsoft SDKs\Windows\v8.1A\Bootstrapper\Packages\Crystal Reports for .NET Framework 4.0`.

You may also have to deal with [this error](https://stackoverflow.com/questions/18463574/setup-has-detected-that-the-file-has-changed-since-it-was-initially-published).