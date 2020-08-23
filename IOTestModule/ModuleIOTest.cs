using IOTestModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IOTestModule
{
    public class ModuleIOTest : IModule
    {
        public ModuleIOTest(IRegionManager regionManager)
        {
            //regionManager.RegisterViewWithRegion("ContentRegion", typeof(IOTestView));
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<IOTestView>();
        }
    }
}
