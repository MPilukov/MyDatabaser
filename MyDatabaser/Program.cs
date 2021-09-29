using MyDatabaser.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace MyDatabaser
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Contains("FilePath"))
            {
                throw new KeyNotFoundException("The file for storing data is not specified");
            }

            var filePath = ConfigurationManager.AppSettings["FilePath"];
            var storage = new FileStorage(filePath);
            var stageStorage = new StageStorage(storage);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(stageStorage));
        }
    }
}
