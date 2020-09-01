using Prism.Mvvm;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Prism.Regions;
using Microsoft.Win32;
using System.IO;
using HETSPrism.Services;
using System.Windows.Forms;
using Prism.Events;
using DataBuilders;
using Prism.Common;
using System.Collections.ObjectModel;

namespace HETSPrism.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public DelegateCommand ImportHomeExercise { get; set; }
        public DelegateCommand<string> ShowResults { get; set; }
        public DelegateCommand<string> RunIOTest { get; set; }
        public DelegateCommand<string> CompilationTest { get; set; }

        private HomeExercisesParser parser;
        
        private string _folderPath;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private bool _canTest;
        private bool _passed;
        public bool CanTest
        {
            get { return _canTest; }
            set { SetProperty(ref _canTest, value); }
        }
        public string FolderPath 
        {
            get { return _folderPath; }
            set { SetProperty(ref _folderPath, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            ImportHomeExercise = new DelegateCommand(ExecuteImportHomeExercise);
            CompilationTest = new DelegateCommand<string>(ExecuteCompilationTest)
                .ObservesCanExecute(() => CanTest);
            RunIOTest = new DelegateCommand<string>(ExecuteRunIOTest)
                .ObservesCanExecute(() => CanTest);
            ShowResults = new DelegateCommand<string>(ExecuteShowResults);
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            CanTest = false;
            _passed = false;
        }
        
        // called when Import Home Exercise clicked
        void ExecuteImportHomeExercise()
        {
            _regionManager.RequestNavigate("ContentRegion", "ResultsView");
            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FolderPath = openFileDialog.SelectedPath;
                parser = new HomeExercisesParser(FolderPath);
                parser.TraverseTree();
                CanTest = true;
            }

            if (parser != null && parser.HomeExercises.Count > 0)
            {
                FolderPath = $"\n Succesfully imported {parser.HomeExercises.Count} Home Exercises";
                _eventAggregator.GetEvent<UpdateHomeExercisesEvent>().Publish(parser.HomeExercises);
                CanTest = true;
            }
            else
            {
                MessageBox.Show("Failed to import home exercises");
            }
        }

        //called when Compilation Test clicked
        private void ExecuteCompilationTest(string uri)
        {
            string message = Services.CompilationTest.StartCompilationTest(parser.HomeExercises);
            if(message != "OK")
            {
                MessageBox.Show(message);
            }
            _eventAggregator.GetEvent<UpdateHomeExercisesEvent>().Publish(parser.HomeExercises);
            _regionManager.RequestNavigate("ContentRegion", uri);
        }

        //called when Run I/O Test clicked
        private void ExecuteRunIOTest(string uri)
        {
            if (parser.HomeExercises != null && !_passed)
            {
                var parameter = new NavigationParameters();
                parameter.Add("homeexercises", parser.HomeExercises);
                parameter.Add("regionManager", _regionManager);
                _passed = true;
                _regionManager.RequestNavigate("ContentRegion", uri, parameter);
            }
            else
            {
                _regionManager.RequestNavigate("ContentRegion", uri);
            }
        }

        //called when Results clicked
        private void ExecuteShowResults(string uri)
        {
            _regionManager.RequestNavigate("ContentRegion", uri);
        }
    }
}
