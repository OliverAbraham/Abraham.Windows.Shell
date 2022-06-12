using System;
using System.Windows;

namespace Abraham.Windows.Shell.Demo
{
	/// <summary>
	/// Demo for nuget package "Abraham.Windows.Shell"
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void AddShortcut(object sender, RoutedEventArgs e)
		{
			AutostartFolder.AddShortcut();
			MessageBox.Show("A shortcut has been created. The next time you logon your program will be started.");
		}

		private void RemoveShortcut(object sender, RoutedEventArgs e)
		{
			AutostartFolder.RemoveShortcut();
			MessageBox.Show("The shortcut has been removed.");
		}

		private void StartBatchfile(object sender, RoutedEventArgs e)
		{
			ExternalPrograms.StartBatchfile("MyBatchfile.cmd");
		}

		private void OpenDirectoryInExplorer(object sender, RoutedEventArgs e)
		{
			ExternalPrograms.OpenDirectoryInExplorer(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
		}

		private void OpenFileInStandardBrowser(object sender, RoutedEventArgs e)
		{
			ExternalPrograms.OpenFileInStandardBrowser("MyDocument.html");
		}

		private void FindAssociatedProgramFor(object sender, RoutedEventArgs e)
		{
			var program = ExternalPrograms.FindAssociatedProgramFor(".html");
			MessageBox.Show($"The program for html files is '{program}'");
		}
	}
}
