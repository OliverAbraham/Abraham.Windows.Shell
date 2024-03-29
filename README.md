# Abraham.Windows.Shell

![](https://img.shields.io/github/oliverabraham/Abraham.Windows.Shell/total) ![](https://img.shields.io/github/license/oliverabraham/Abraham.Windows.Shell) ![](https://img.shields.io/github/languages/count/oliverabraham/Abraham.Windows.Shell) ![GitHub Repo stars](https://img.shields.io/github/stars/oliverabraham/Abraham.Windows.Shell?label=repo%20stars) ![GitHub Repo stars](https://img.shields.io/github/stars/oliverabraham?label=user%20stars)


## OVERVIEW

This library provides methods to create links to your program in Windows Autostart folder.
And to open file with the associated program, associated browser or to start a batch file.



## LICENSE

Licensed under Apache licence.
https://www.apache.org/licenses/LICENSE-2.0


## Compatibility

The nuget package was build with DotNET 6.



## INSTALLATION

Install the Nuget package "Abraham.Windows.Shell" into your application (from https://www.nuget.org).



## HOW TO INSTALL A NUGET PACKAGE
This is very simple:
- Start Visual Studio (with NuGet installed) 
- Right-click on your project's References and choose "Manage NuGet Packages..."
- Choose Online category from the left
- Enter the name of the nuget package to the top right search and hit enter
- Choose your package from search results and hit install
- Done!


or from NuGet Command-Line:

    Install-Package Abraham.Windows.Shell





## AUTHOR

Oliver Abraham, mail@oliver-abraham.de, https://www.oliver-abraham.de

Please feel free to comment and suggest improvements!



## SOURCE CODE

The source code for this nuget package is hosted at:

https://github.com/OliverAbraham/Abraham.Windows.Shell

The Nuget Package is hosted at: 

https://www.nuget.org/packages/Abraham.Windows.Shell



## Examples

For examples refer to the demo project on github. It demonstrates all methods.


## Getting started

Add the Nuget package "Abraham.Windows.Shell" to your project.


### To add a link to the autostart folder

```C#
AutostartFolder.AddShortcut();
```

### To remove the link

```C#
AutostartFolder.RemoveShortcut();
```

### To start a batchfile (bat, cmd or ps1)

```C#
ExternalPrograms.StartBatchfile("MyBatchfile.cmd");
```

### To open a windows explorer with a given folder

```C#
ExternalPrograms.OpenDirectoryInExplorer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
```

### To open a file with the associated program

```C#
ExternalPrograms.OpenFileInStandardBrowser("MyDocument.html");
```

### To find the associated program for a file extension

```C#
var program = ExternalPrograms.FindAssociatedProgramFor(".html");
```



## SCREENSHOTS

![](Screenshots/screenshot1.jpg)


# MAKE A DONATION !

If you find this application useful, buy me a coffee!
I would appreciate a small donation on https://www.buymeacoffee.com/oliverabraham

<a href="https://www.buymeacoffee.com/app/oliverabraham" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/v2/default-yellow.png" alt="Buy Me A Coffee" style="height: 60px !important;width: 217px !important;" ></a>
