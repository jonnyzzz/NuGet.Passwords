NuGet.Passwords
================

Small handy NuGet usage helper.

To make it work, first, 
close ALL Visual Studio instances.

Available as NuGet Tool Package, see
https://nuget.org/packages/NuGet.Passwords.Persist


License: Apache 2.0
===================

This is a add-in to NuGet console that 
makes NuGet remember password for feed.

In /tools/ folder of NuGet package you'll
find the plugin and nuget-ex.bat that would
make NuGet.exe start with plugin. 
It's supposed you have NuGet.exe avaible in 
system PATH.

In case you do not have NuGet.exe, simple
install NuGet.CommandLine package. 

For usage details, see NuGet-ex.bat help AuthorizeFeed

