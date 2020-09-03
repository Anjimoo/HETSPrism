using Xunit;
using DataBuilders;
using HETSPrism.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HETSPrismTests

{
    public class MainWindowViewModelTests
    {
        private readonly IRegionManager _regionManager = new RegionManager();
        private  readonly IEventAggregator _eventAggregator = new EventAggregator();
        [Fact]
        public void ExecuteImportHomeExerciseTest()
        {
       


        }
    }
}