using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Shapes;

namespace IOTestModule.Services
{
    public class InputOutputParser
    {
        public static string Parser(string filePath)
        {
            string text = File.ReadAllText(filePath);
            return text;
        }
    }
}
