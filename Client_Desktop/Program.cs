﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Client_Desktop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ExecuteCommand("SqlLocalDB start HARVESTDATABASE.mdf");
            HarvestForm mainForm = null;
            try
            {
                mainForm = new HarvestForm();
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Installation successful, please relaunch Harvest.");
                Core.Utilities.Logging.Logger.Log(ex);
                mainForm.Dispose();
            }            
        }

        static void ExecuteCommand(string command)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            var process = Process.Start(processInfo);

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("output>>" + e.Data);
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("error>>" + e.Data);
            process.BeginErrorReadLine();

            process.WaitForExit();

            Console.WriteLine("ExitCode: {0}", process.ExitCode);
            process.Close();
        }
    }
}
