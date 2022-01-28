using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Abraham.Windows.Shell
{
    /// <summary>
    /// Provides methods to open file with the associated program, associated browser or to start a batch file.
    /// </summary>
    ///
    /// <remarks>
    /// Oliver Abraham, www.oliver-abraham.de, mail@oliver-abraham.de
    /// </remarks>
    public class ExternalPrograms
    {
        #region ------------- Methods -------------------------------------------------------------

        /// <summary>
        /// Starts an external process with the given filename and arguments
        /// </summary>
        public static void StartProcess(string program, string arguments)
        {
            Process.Start(program, arguments);
        }

        /// <summary>
        /// Starts a batchfile in cmd.exe or powershell.exe interpreter
        /// </summary>
        public static void StartBatchfile(string fullPathToBatchfile)
		{
			string currentDirectory = Directory.GetCurrentDirectory();
			StartBatchfile_internal(fullPathToBatchfile);
			Directory.SetCurrentDirectory(currentDirectory);
		}

        /// <summary>
        /// Opens up a windows explorer with the given directory
        /// </summary>
		public static void OpenDirectoryInExplorer(string directoryName)
        {
            if (Directory.Exists(directoryName) == false)
                throw new Exception("path does not exist");

            string Programm = "explorer.exe";
            Process.Start(Programm, " \"" + directoryName + "\"");
        }

        /// <summary>
        /// Opens a file with the associated browser
        /// </summary>
        public static void OpenFileInStandardBrowser(string filename)
        {
            var extension = Path.GetExtension(filename);
            string program = FindAssociatedProgramFor(extension);
            if (program == null)
                return;

            Process.Start(program, " \"" + filename + "\"");
        }

        /// <summary>
        /// Opens a file with the associated editor
        /// </summary>
        public static void OpenFileInAssociatedEditor(string filename)
        {
            var extension = Path.GetExtension(filename);
            string program = FindAssociatedProgramFor(extension);
            if (program == null)
                return;

            bool bracketsAreAlreadyThere = (program.Contains("\"%1\"") || program.Contains("\" %1\""));
            string filenameInBrackets = bracketsAreAlreadyThere 
                ? filename
                : $"\"{filename}\"";

            var parts = program.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.GetLength(0) > 0)
			{
                var exeFile = parts[0];
                Process.Start(exeFile, filenameInBrackets);
			}
        }

        /// <summary>
        /// Looks up the associated program for a given file extension
        /// </summary>
		public static string FindAssociatedProgramFor(string extension)
		{
            var docName = Registry.GetValue($@"HKEY_CLASSES_ROOT\{extension}", string.Empty, string.Empty) as string;
            if (!string.IsNullOrEmpty(docName))
            {
                var associatedProgram = Registry.GetValue($@"HKEY_CLASSES_ROOT\{docName}\shell\open\command", string.Empty, string.Empty) as string;
                return associatedProgram;
            }
            return null;
        }
		#endregion



		#region ------------- Implementation ------------------------------------------------------
		private static void StartBatchfile_internal(string fullPathToBatchfile)
		{
			string workingDirectory = Path.GetPathRoot(fullPathToBatchfile);
			Directory.SetCurrentDirectory(workingDirectory);

            var extension = Path.GetExtension(fullPathToBatchfile);

            var fileIsAPowershellBatch = extension.EndsWith("ps1");
            var fileIsACmdBatch = extension.EndsWith("cmd") || extension.EndsWith("bat");

            var commandShell = fileIsAPowershellBatch 
                ? "powershell.exe"
                : fileIsACmdBatch 
                    ? "cmd.exe"
                    : throw new Exception($"unsupported file type '{extension}'");

			Process.Start(commandShell, " /k \"" + fullPathToBatchfile + "\"");
		}
		#endregion
	}
}
