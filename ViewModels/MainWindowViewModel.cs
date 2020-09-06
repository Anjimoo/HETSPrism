using Prism.Mvvm;
using Prism.Commands;
using System;
using Prism.Regions;
using HETSPrism.Services;
using Prism.Events;
using DataBuilders;


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
        private readonly IDialogService _dialogService;
        private readonly IProgress<double> _runTestProgress;
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

        private double _progressBarPercentage;
        public double ProgressBarPercentage
        {
            get { return _progressBarPercentage; }
            set { SetProperty(ref _progressBarPercentage, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator,
            IDialogService dialogService)
        {
            _runTestProgress = new Progress<double>(
                (index) => { ProgressBarPercentage = index; });
            ImportHomeExercise = new DelegateCommand(ExecuteImportHomeExercise);
            CompilationTest = new DelegateCommand<string>(ExecuteCompilationTest)
                .ObservesCanExecute(() => CanTest);
            RunIOTest = new DelegateCommand<string>(ExecuteRunIOTest)
                .ObservesCanExecute(() => CanTest);
            ShowResults = new DelegateCommand<string>(ExecuteShowResults);
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
            CanTest = false;
            _passed = false;
        }
        
        // called when Import Home Exercise clicked
        void ExecuteImportHomeExercise()
        {
            _regionManager.RequestNavigate("ContentRegion", "IOTestView");
            _regionManager.RequestNavigate("ContentRegion", "ResultsView");
            //get folder path from user
            FolderPath = _dialogService.ShowFolderBrowserDialog();
            if (FolderPath != null)
            {
                parser = new HomeExercisesParser(FolderPath);
                parser.TraverseTree();
            }

            if (parser != null && parser.HomeExercises.Count > 0)
            {
                FolderPath = $"\nSuccesfully imported {parser.HomeExercises.Count} Home Exercises";
                _eventAggregator.GetEvent<UpdateHomeExercisesEvent>().Publish(parser.HomeExercises);
                CanTest = true;
            }
            else
            {
                _dialogService.ShowMessageBox("Failed to import home exercises");
            }
        }

        //called when Compilation Test clicked
        private async void ExecuteCompilationTest(string uri)
        {
            try
            {
                CanTest = false;
                string message = await Services.CompilationTest.StartCompilationTest(parser.HomeExercises, _runTestProgress);
                if (message != "OK")
                {
                    _dialogService.ShowMessageBox(message);
                }

                _eventAggregator.GetEvent<UpdateHomeExercisesEvent>().Publish(parser.HomeExercises);
                _regionManager.RequestNavigate("ContentRegion", uri);
            }
            catch (Exception e)
            {
                _dialogService.ShowMessageBox(e.Message);
            }
            finally
            {
                CanTest = true;
                ProgressBarPercentage = 0;
            }
        }

        //called when Run I/O Test clicked
        private void ExecuteRunIOTest(string uri)
        {
            _regionManager.RequestNavigate("ContentRegion", uri);
        }

        //called when Results clicked
        private void ExecuteShowResults(string uri)
        {
            _regionManager.RequestNavigate("ContentRegion", uri);
        }
    }
}
