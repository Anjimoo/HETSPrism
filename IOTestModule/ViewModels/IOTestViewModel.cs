using DataBuilders;
using IOTestModule.Services;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Windows;


namespace IOTestModule.ViewModels
{
    public class IOTestViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        public DelegateCommand StartTest { get; set; }
        public DelegateCommand AddIOFiles { get; set; }

        private bool _canStartTest;
        public bool CanStartTest
        {
            get { return _canStartTest; }
            set { SetProperty(ref _canStartTest, value); }
        }

        private int _numberOfSecondsToWait;
        public int NumberOfSecondsToWait
        {
            get { return _numberOfSecondsToWait; }
            set { SetProperty(ref _numberOfSecondsToWait, value); }
        }

        public DelegateCommand<InputOutputModel> AddOutputFile { get; set; }
        public ObservableCollection<InputOutputModel> InputOutputModels { get; set; }

        private ObservableCollection<HomeExercise> _homeExercises;

        public IOTestViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            CanStartTest = true;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<UpdateHomeExercisesEvent>().Subscribe(UpdatedHomeExercises);
            StartTest = new DelegateCommand(ExecuteStartTest)
                .ObservesCanExecute(() => CanStartTest);
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
                var tempIOModel = new InputOutputModel
                {
                    InputTextFullPath = System.IO.Path.GetFullPath(openFileDialog.FileName),
                    InputText = InputOutputParser.Parser(openFileDialog.FileName)
                };
                    ExecuteAddOutputFile(tempIOModel);
            }
        }

        //called on Start Test click
        private async void ExecuteStartTest()
        {
            try
            {
                CanStartTest = false;
                await RunTest.StartRunTest(_homeExercises, InputOutputModels, NumberOfSecondsToWait, 
                    _eventAggregator);
                // change view to ResultsView and publish changes in _homeExercises
                _eventAggregator.GetEvent<UpdateHomeExercisesEvent>().Publish(_homeExercises);
                _regionManager.RequestNavigate("ContentRegion", "ResultsView");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                CanStartTest = true;
                _eventAggregator.GetEvent<UpdateProgressBarEvent>().Publish(0);
            }
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
