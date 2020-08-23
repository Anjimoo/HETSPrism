using Prism.Ioc;
using Prism.Modularity;
using ResultsModule.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResultsModule
{
    public class ModuleResults : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ResultsView>();
        }
    }
}
