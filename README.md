# Abraham.Windows.Shell

Oliver Abraham
mail@oliver-abraham.de


## Abstract

This library provides methods to create a linkto your program 
in the Windows Windows Autostart folder.
Andto open file with the associated program, associated browser 
or to start a batch file.


## License

Licensed under GPL v3 license.
https://www.gnu.org/licenses/gpl-3.0.en.html


## Compatibility

The nuget package was build with DotNET 6.


## Hosted at

The source code for this nuget package is hosted at:
https://github.com/OliverAbraham/Abraham.Windows.Shell


## Examples

For examples refer to the demo project on github. It demonstrates all methods.


## Getting started

Add the Nuget package "Abraham.Windows.Shell" to your project.


### Adding a link to the autostart folder

		AutostartFolder.AddShortcut();

### Removing the link

		AutostartFolder.RemoveShortcut();

### Starting a batchfile (bat, cmd or ps1)

		ExternalPrograms.StartBatchfile("MyBatchfile.cmd");

### Opening windows explorer with a given folder

		ExternalPrograms.OpenDirectoryInExplorer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

### Opening a file with the associated program

		ExternalPrograms.OpenFileInStandardBrowser("MyDocument.html");

### Finding the associated program for a file extension

		var program = ExternalPrograms.FindAssociatedProgramFor(".html");
