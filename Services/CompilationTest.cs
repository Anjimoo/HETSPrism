using DataBuilders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace HETSPrism.Services
{
    public static class CompilationTest
    {

        public static void StartCompilationTest(List<HomeExercise> homeExercises)
        {
            foreach(var homeExercise in homeExercises)
            {
                // need try catch
                Process process = new Process();
                process.StartInfo.FileName = "javac.exe";
                process.StartInfo.Arguments = $"-Xlint {homeExercise.HomeExercisePath}";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.Start();

                StreamReader sr = process.StandardOutput;
                string output = sr.ReadToEnd();
                StreamReader se = process.StandardError;
                string error = se.ReadToEnd();
                process.WaitForExit();
                
            }
        }
    }
}
