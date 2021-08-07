using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace EasySave.LibraryV3.Models
{

    public class ManageThread
    {
        public bool IsWorkSoftRunning { get; set; }
        public bool EndCopy { get; set; }
        public Mutex m { get; set; }
        private Thread pause { get; set; }


        public ManageThread()
        {
            EndCopy = false;
        }


        public void InitiallizePauseSoft(string _ProcessSoftWork)
        {
            _ProcessSoftWork = ConvertPathToProcess(_ProcessSoftWork);
            pause = new Thread(() =>
            {
                while (!EndCopy)
                {
                    VerifSoftWork(_ProcessSoftWork);
                    Thread.Sleep(100);
                }
            });
            pause.Start();
        }


        private void VerifSoftWork(string _ProcessSoftWork)
        {
            if (Process.GetProcessesByName(_ProcessSoftWork).Length > 0)
            {
                IsWorkSoftRunning = true;
            }
            else
            {
                IsWorkSoftRunning = false;
            }
        }

        private string ConvertPathToProcess(string _path)
        {
            char[] cut = { '\\', '.' };
            string[] cutpath = _path.Split(cut);
            string processName = cutpath[cutpath.Length - 2];
            return processName;
        }


        public bool MonoInstance()
        {
            bool isnew;
            m = new Mutex(true, "EasySaveInstance", out isnew);
            if (!isnew)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void CloseMutex()
        {
            m.ReleaseMutex();
            m.Close();
        }

        public void PauseSoft()
        {
            File.Create("./Pause.txt");
        }

        public void RunSoft()
        {
            if (File.Exists("./Pause.txt"))
            {
                File.Delete("./Pause.txt");
            }
        }
    }
}
