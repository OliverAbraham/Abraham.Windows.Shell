// Using IWshRuntimeLibrary requires a COM reference to 'Windows Script Host Object Model'
// ATTENTION: change the property "Embed Interop-types" to false!!!
using IWshRuntimeLibrary;

// Using Shell32 requires a COM reference to 'Microsoft Shell Controls and Automation'
// ATTENTION: change the property "Embed Interop-types" to false!!!
using Shell32;

using Path = System.IO.Path;
using System.IO;


namespace Abraham.Windows.Shell
{
    /// <summary>
    /// Creates and removes links to your program in the Windows Autostart folder
    /// </summary>
    ///
    /// <remarks>
    /// Oliver Abraham, www.oliver-abraham.de, mail@oliver-abraham.de
    /// </remarks>
    ///
    /// <remarks>
    /// ATTENTION: 
    /// To compile this code, you need to add two references to your project:
    /// Go to 'add reference" and COM --> Type libraries and add:
    ///   1) 'Windows Script Host Object Model'
    ///   2) 'Microsoft Shell Controls and Automation'
    ///
    /// Check that you see in your project:
    ///   1) Interop.IWshRuntimeLibrary
    ///   2) Interop.Shell32
    ///   
    /// Then open up the properties of both references and change the property "Embed Interop-types" to false!!!
    /// </remarks>
    public class AutostartFolder
    {
        #region ------------- Properties ----------------------------------------------------------
        public string ProductName { get; set; }
        public string Description { get; set; }
        #endregion



        #region ------------- Init ----------------------------------------------------------------
        public AutostartFolder()
        {
            ProductName = "MyProduct";
            Description = "Launch My Application";
        }

        public AutostartFolder(string productName, string description)
        {
            ProductName = productName;
            Description = description;
        }
        #endregion



        #region ------------- Methods -------------------------------------------------------------
        
        /// <summary>
        /// Create a new shortcut in windows autostart folder, pointing to your program.
        /// The next time windows starts it will execute your program.
        /// </summary>
        /// 
        /// <param name="pathToMyExe">
        /// The full path to your program, i.e. C:\Program Files\MyCompany\MyApplication.exe
        /// If you pass null it will take the filename of the current process.
        /// </param>
        /// 
        /// <param name="appIconLocation">
        /// Optional path to you application icon, i.e. C:\Program Files\MyCompany\MyApplication.ico
        /// </param>
        public void CreateShortcut(string pathToMyExe = null, string appIconLocation = null)
        {
            WshShellClass wshShell = new WshShellClass();
            IWshRuntimeLibrary.IWshShortcut shortcut;
            string startUpFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            // Create the shortcut
            string Dateiname = startUpFolderPath + "\\" + ProductName + ".lnk";
            shortcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(Dateiname);

            if (pathToMyExe == null)
                pathToMyExe = GetTargetExeName();
            shortcut.TargetPath = pathToMyExe;
            shortcut.WorkingDirectory = Path.GetDirectoryName(shortcut.TargetPath);
            shortcut.Description = Description;

            if (appIconLocation != null)
                shortcut.IconLocation = appIconLocation;
            shortcut.Save();
        }

        /// <summary>
        /// Removes all shortcuts from windows autostart folder that point to you program.
        /// </summary>
        /// 
        /// <param name="pathToMyExe">
        /// The full path to your program, i.e. C:\Program Files\MyCompany\MyApplication.exe
        /// If you pass null it will take the filename of the current process.
        /// </param>
        public void DeleteShortcut(string pathToMyExe = null)
        {
            if (pathToMyExe == null)
                pathToMyExe = GetTargetExeName();

            string startUpFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            DirectoryInfo di = new DirectoryInfo(startUpFolderPath);
            FileInfo[] files = di.GetFiles("*.lnk");

            foreach (FileInfo fi in files)
            {
                string shortcutTargetFile = GetTargetFilenameByShortcutFilename(fi.FullName);

                if (shortcutTargetFile.EndsWith(pathToMyExe, StringComparison.InvariantCultureIgnoreCase))
                {
                    System.IO.File.Delete(fi.FullName);
                }
            }
        }

        /// <summary>
        /// Get the target filename by the name of the shortcut in windows autostart folder.
        /// </summary>
        /// 
        /// <param name="pathToMyExe">
        /// The full path to the shortcut file, i.e. C:\Users\__YOUR_USERNAME__\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\notepad++.lnk
        /// </param>
        public string GetTargetFilenameByShortcutFilename(string shortcutFilename)
        {
            string pathOnly = Path.GetDirectoryName(shortcutFilename);
            string filenameOnly = Path.GetFileName(shortcutFilename);

            Shell32.Shell shell = new Shell32.ShellClass();
            Shell32.Folder folder = shell.NameSpace(pathOnly);
            Shell32.FolderItem folderItem = folder.ParseName(filenameOnly);
            if (folderItem != null)
            {
                Shell32.ShellLinkObject link = (Shell32.ShellLinkObject)folderItem.GetLink;
                return link.Path;
            }

            return String.Empty;
        }
        #endregion



        #region ------------- Implementation ------------------------------------------------------
        private static string GetTargetExeName()
        {
            string ExeName = Environment.GetCommandLineArgs()[0];
            if (ExeName.Contains(".vshost.exe"))
                ExeName = ExeName.Replace(".vshost.exe", ".exe");
            return ExeName;
        }
        #endregion
    }
}
