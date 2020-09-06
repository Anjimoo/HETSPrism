using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Prism.Events;

namespace DataBuilders
{
    public class UpdateProgressBarEvent : PubSubEvent<double>
    {
    }
}
