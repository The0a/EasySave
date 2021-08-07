using EasySave_V3Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EasySave_V3Library.ViewModels
{
    public class ActionJob
    {
        public Job JobParam { get; set; }

        public ActionJob(Job job)
        {
            JobParam = job;
        }

        public bool CreateJob()
        {
            return JobParam.CreateFileJob();
            //Verification Possible du type d'erreur lors de la création -> Return Dictionnaire <Bool, StatusDescr>
        }

        public bool ModifyJob(string name, string pathS, string pathD, bool differantial)
        {
            return JobParam.ModifyJob(name, pathS, pathD, differantial);
        }

        public bool ExecuteJob()
        {
            return JobParam.ExecuteJob();
        }

        public bool DeleteJob()
        {
            return JobParam.DeleteJob();
        }

    }
}
