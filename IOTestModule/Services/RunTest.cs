﻿using DataBuilders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using System.Threading.Tasks;
using Prism.Events;


namespace IOTestModule.Services
{
    public static class RunTest
    {
        public static async Task StartRunTest(IReadOnlyList<HomeExercise> homeExercises,
            IReadOnlyList<InputOutputModel> inputOutputModels, int numberOfSecondsToWait, IEventAggregator eventAggregator=null)
        {
            for (var index = 0; index < homeExercises.Count; index++)
            {
                var homeExercise = homeExercises[index];
                homeExercise.CompatibleRunTestList = new List<string>();
                foreach (var inputOutputModel in inputOutputModels)
                {
                    Process process = new Process();
                    process.StartInfo.FileName = @"java.exe";
                    // arguments actually will not work if program does not need arguments and instead wants input in running time
                    process.StartInfo.Arguments =
                        $"{homeExercise.HomeExercisePath} < {inputOutputModel.InputTextFullPath}";
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
                        sw.Write(inputOutputModel.InputText);
                    }

                    // needed to see output from program
                    using (StreamReader srOutput = process.StandardOutput)
                    {
                        string output = null;
                        var readOutputTask = Task.Run(() => { output = srOutput.ReadToEnd(); });
                        await Task.WhenAny(readOutputTask, Task.Delay(TimeSpan.FromSeconds(numberOfSecondsToWait)));
                        if (output == null)
                        {
                            homeExercise.RunTestErrorOutput = $"Exercise didn't stopped after {numberOfSecondsToWait} seconds";
                            homeExercise.IsRunTestOk = "Run time error";
                            process.Kill();
                            eventAggregator.GetEvent<UpdateProgressBarEvent>().Publish(((double)(index + 1) / homeExercises.Count) * 100);
                            //progress?.Report(((double)(index + 1) / homeExercises.Count) * 100);
                            continue;
                        }
                        homeExercise.RunTestOutput = output;
                    }

                    // needed to see errors from program
                    using (StreamReader srError = process.StandardError)
                    {
                        homeExercise.RunTestErrorOutput = srError.ReadToEnd();
                        if (homeExercise.RunTestErrorOutput != "")
                        {
                            homeExercise.IsRunTestOk = "Has error";
                        }else if (homeExercise.IsRunTestOk != "Run time error")
                        {
                            homeExercise.IsRunTestOk = "Success";
                        }
                    }

                    CompareOutputs(inputOutputModel, homeExercise);
                }

                CompatibleChecker(homeExercise);
                eventAggregator.GetEvent<UpdateProgressBarEvent>().Publish(((double)(index + 1) / homeExercises.Count) * 100);
                //progress?.Report(((double)(index+1)/homeExercises.Count) * 100);
            }
        }

        private static void CompareOutputs(InputOutputModel inputOutputModel, HomeExercise homeExercise)
        {
            //equal and ignore from symbols (white space etc...)
            if (String.Compare(inputOutputModel.OutputText, homeExercise.RunTestOutput,
                CultureInfo.CurrentCulture, CompareOptions.IgnoreSymbols) == 0)
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

        private static void CompatibleChecker(HomeExercise homeExercise)
        {
            foreach (var compatibleRunTest in homeExercise.CompatibleRunTestList)
            {
                if (compatibleRunTest == "not compatible")
                {
                    homeExercise.IsCompatibleRunTest = "Not compatible";
                    break;
                }
                else
                {
                    homeExercise.IsCompatibleRunTest = "Compatible";
                }
            }
        }
    }
}
