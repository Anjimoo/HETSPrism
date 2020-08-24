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

        public static string StartCompilationTest(List<HomeExercise> homeExercises)
        {
            foreach(var homeExercise in homeExercises)
            {
                Process process = new Process();
                process.StartInfo.FileName = "javac.exe";
                process.StartInfo.Arguments = $"-Xlint {homeExercise.HomeExercisePath}";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                try
                {
                    process.Start();
                }
                catch
                {
                    return "Error: javac.exe compiler not found in path variables";
                }
                StreamReader sr = process.StandardOutput;
                homeExercise.CompilationOutput = sr.ReadToEnd();
                StreamReader se = process.StandardError;
                homeExercise.CompilationErrorOutput = se.ReadToEnd();
                process.WaitForExit();
                
            }
            return "OK";
        }
    }
}
