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

        public MainWindowViewModel(IRegionManager regionManager)
        {
            ImportHomeExercise = new DelegateCommand(ExecuteImportHomeExercise);
            CompilationTest = new DelegateCommand<string>(ExecuteCompilationTest)
                .ObservesCanExecute(() => CanTest);
            RunIOTest = new DelegateCommand<string>(ExecuteRunIOTest)
                .ObservesCanExecute(() => CanTest);
            ShowResults = new DelegateCommand<string>(ExecuteShowResults);
            _regionManager = regionManager;
            CanTest = false;
            _passed = false;
        }

        void ExecuteImportHomeExercise()
        {
            //CanTest = true;
            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FolderPath = openFileDialog.SelectedPath;
                parser = new HomeExercisesParser(FolderPath);
                CanTest = true;
            }
            if (parser != null && parser.HomeExercises.Count > 0)
            {
                FolderPath = $"\n Succesfully imported {parser.HomeExercises.Count} Home Exercises";
                CanTest = true;
            }
            else
            {
                FolderPath = "Failed to import home exercises";
            }
        }

        private void ExecuteCompilationTest(string uri)
        {
            //Services.CompilationTest.StartCompilationTest(parser.HomeExercises, CompilerPath);
            _regionManager.RequestNavigate("ContentRegion", uri);
        }

        private void ExecuteRunIOTest(string uri)
        {
            if (parser.HomeExercises != null && !_passed)
            {
                var parameter = new NavigationParameters();
                parameter.Add("homeexercises", parser.HomeExercises);
                _passed = true;
                _regionManager.RequestNavigate("ContentRegion", uri, parameter);
            }
            else
            {
                _regionManager.RequestNavigate("ContentRegion", uri);
            }
        }
        private void ExecuteShowResults(string uri)
        {
            _regionManager.RequestNavigate("ContentRegion", uri);
        }
    }
}
