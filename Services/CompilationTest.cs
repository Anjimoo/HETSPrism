﻿using DataBuilders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace HETSPrism.Services
{
    public static class CompilationTest
    {

        public static string StartCompilationTest(ObservableCollection<HomeExercise> homeExercises)
        {
            foreach(var homeExercise in homeExercises)
            {
                
                // definition of process
                Process process = new Process();
               process.StartInfo.FileName = "C:\\Program Files\\Java\\jdk-14.0.1\\bin\\javac.exe";
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
                //return compilation output
                homeExercise.CompilationErrorOutput = se.ReadToEnd();


            }
            return "OK";
        }
    }
}
