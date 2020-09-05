using DataBuilders;
using IOTestModule.Services;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Common;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls.Ribbon;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace IOTestModule.Services
{
    public static class RunTest
    {
        public static void StartRunTest(ObservableCollection<HomeExercise> _homeExercises, ObservableCollection<InputOutputModel> InputOutputModels)
        {
            foreach (var homeExercise in _homeExercises)
            {
                homeExercise.CompatibleRunTestList = new List<string>();              
                for (int i = 0; i < InputOutputModels.Count; i++)
                {
                    Process process = new Process();
                    process.StartInfo.FileName = @"java.exe";
                    // arguments actually will not work if program does not need arguments and instead wants input in running time
                    process.StartInfo.Arguments = $"{homeExercise.HomeExercisePath} < {InputOutputModels[i].InputTextFullPath }";
                    //process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.RedirectStandardInput = true;

                    try
                    {
                        process.Start();
                    }
                    catch (InvalidCastException e)
                    {
                        throw new InvalidCastException("Error: java.exe compiler not found in path variables", e);
                    }
                    // needed if java Program wants some enter input
                    using (StreamWriter sw = process.StandardInput)
                    {
                        sw.Write(InputOutputModels[i].InputText);
                    }
                    // needed to see output from program
                    using (StreamReader srOutput = process.StandardOutput)
                    {
                        homeExercise.RunTestOutput = srOutput.ReadToEnd();
                    }
                    // needed to see errors from program
                    using (StreamReader srError = process.StandardError)
                    {
                        homeExercise.RunTestErrorOutput = srError.ReadToEnd();
                        if (homeExercise.RunTestErrorOutput != "")
                        {
                            homeExercise.IsRunTestOk = "No";
                        }
                    }

                    //equal and ignore from symbols (white space etc...)
                    if (String.Compare(InputOutputModels[i].OutputText, homeExercise.RunTestOutput, CultureInfo.CurrentCulture, CompareOptions.IgnoreSymbols)==0)
                    {
                        //do something when the output's compatible 
                        homeExercise.CompatibleRunTestList.Add("compatible");
                    }
                    else
                    {
                        //do something when the output's not compatible 
                        homeExercise.CompatibleRunTestList.Add("not compatible");
                    }

                }
                foreach (var compatibleRunTest in homeExercise.CompatibleRunTestList)
                { 
                    if(compatibleRunTest == "not compatible")
                    {
                        homeExercise.IsCompatibleRunTest = "No";
                        break;
                    }
                    else
                    {
                        homeExercise.IsCompatibleRunTest = "Yes";
                    }
                }

            }

        }
    }
}
