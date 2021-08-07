using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EasySave_V3.Library.ViewModels
{
    public class SoftRunning
    {
        public bool IsSoftRunning(string _path)
        {
            _path = ConvertPathToProcess(_path);
            if (Process.GetProcessesByName(_path).Length > 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        private string ConvertPathToProcess(string _path)
        {
            char[] cut = { '\\', '.' };
            string[] cutpath = _path.Split(cut);
            string processName = cutpath[cutpath.Length - 2];
            return processName;
        }
    }
}
