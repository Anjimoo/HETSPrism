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

namespace IOTestModule.ViewModels
{
    public class IOTestViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        public DelegateCommand StartTest { get; set; }
        public DelegateCommand AddIOFiles { get; set; }
        public DelegateCommand<InputOutputModel> AddOutputFile { get; set; }

        private ObservableCollection<HomeExercise> _homeExercises;

        private IRegionManager _regionManager;
        
        private bool _checkCompatibility;
        public bool CheckCompatibility
        {
            get { return _checkCompatibility; }
            set { SetProperty(ref _checkCompatibility, value); }
        }

        public ObservableCollection<InputOutputModel> InputOutputModels { get; set; }

        public IOTestViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<UpdateHomeExercisesEvent>().Subscribe(UpdatedHomeExercises);
            StartTest = new DelegateCommand(ExecuteStartTest, CanExecuteStartTest)
                .ObservesProperty(() => CheckCompatibility);
            AddIOFiles = new DelegateCommand(ExecuteAddIOFiles);
            AddOutputFile = new DelegateCommand<InputOutputModel>(ExecuteAddOutputFile);
            InputOutputModels = new ObservableCollection<InputOutputModel>();
            _homeExercises = new ObservableCollection<HomeExercise>();
        }

        //called on Add Output File click
        private void ExecuteAddOutputFile(InputOutputModel inputOutputModel)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                string output = InputOutputParser.Parser(openFileDialog.FileName);
                if (inputOutputModel != null) {
                    inputOutputModel.OutputText = output;
                    InputOutputModels.Add(inputOutputModel);
                }
                else
                {
                    InputOutputModels.Add(new Services.InputOutputModel { OutputText = output });
                }
            }
        }

        //called on Add I/O files click
        private void ExecuteAddIOFiles()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var tempIOModel = new InputOutputModel {
                    InputTextClient = System.IO.Path.GetFullPath(openFileDialog.FileName)
                ,
                    InputText = InputOutputParser.Parser(openFileDialog.FileName)
                };
                ExecuteAddOutputFile(tempIOModel);
            }
        }

        private bool CanExecuteStartTest()
        {
            return CheckCompatibility;
        }

        //called on Start Test click
        private void ExecuteStartTest()
        {
            foreach (var homeExercise in _homeExercises)
            {
                for (int i = 0; i < InputOutputModels.Count; i++)
                {
                    //string input = homeExercise.HomeExercisePath;
                    //int index = input.IndexOf(".");
                    //string HomeExercisePathWithNotDot = input.Substring(0, index);

                    Process process = new Process();
                    process.StartInfo.FileName = @"java.exe";
                    // arguments actually will not work if program does not need arguments and instead wants input in running time
                    process.StartInfo.Arguments = $"{homeExercise.HomeExercisePath} < {InputOutputModels[i].InputText }";
                    //process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
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
                    using (StreamWriter sw = process.StandardInput){
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
                    }

                    //return error compilation output
                    //return compilation output
                    //var output = new List<string>();
                    //while (process.StandardOutput.Peek() > -1)
                    //{
                    //    output.Add(process.StandardOutput.ReadLine());
                    //}

                    //while (process.StandardError.Peek() > -1)
                    //{
                    //    output.Add(process.StandardError.ReadLine());
                    //}
                    


                    //if (InputOutputModels[i].OutputText == homeExercise.CompilationOutput)
                    //{
                    //    //return i/o succeed
                    //}

                }

            }

            //TODO use _homeExercises like List<HomeExercise>



            // change view to ResultsView and publish changes in _homeExercises
            _eventAggregator.GetEvent<UpdateHomeExercisesEvent>().Publish(_homeExercises);
            _regionManager.RequestNavigate("ContentRegion", "ResultsView");
        }

        //get parameters from another screen
        public void DataReceived(object sender, DataReceivedEventArgs e)
        {

        }

        private void UpdatedHomeExercises(ObservableCollection<HomeExercise> homeExercises)
        {
            _homeExercises.Clear();
            foreach (var homeExercise in homeExercises)
            {
                _homeExercises.Add(homeExercise);
            }
        }
    }
}
