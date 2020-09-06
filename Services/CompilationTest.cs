﻿using DataBuilders;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace HETSPrism.Services
{
    public static class CompilationTest
    {

        public static async Task<string> StartCompilationTest(ObservableCollection<HomeExercise> homeExercises)
        {
            foreach(var homeExercise in homeExercises)
            {
                
                // definition of process
                Process process = new Process();
                process.StartInfo.FileName = "javac.exe";
                process.StartInfo.Arguments = $"-Xlint {homeExercise.HomeExercisePath}";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                try
                {
                    process.Start();
                }
                catch
                {
                    return "Error: javac.exe compiler not found in path variables";
                }

                StreamReader se = process.StandardError;
                homeExercise.CompilationErrorOutput = await se.ReadToEndAsync();
                if (homeExercise.CompilationErrorOutput != "")
                {
                    homeExercise.IsCompilationTestOk = "No";
                }
                else
                {
                    homeExercise.IsCompilationTestOk = "Yes";
                }
            }
            return "OK";
        }
    }
}
