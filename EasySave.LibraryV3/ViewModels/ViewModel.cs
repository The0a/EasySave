using EasySave.LibraryV3.Models;
using EasySave_V3.Library.Models;
using EasySave_V3Library.Models;
using EasySave_V3Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EasySave_V3.Library.ViewModels
{
    public class ViewModel
    {
        public Job[] Jobs { get; set; }
        public InitializeJob IJ { get; set; }
        public SetupProgramFile Setup { get; set; }
        public Verification Verif { get; set; }
        public GeneralParam GP { get; set; }
        public SoftRunning SR { get; set; }
        public ManageThread MThread { get; set; }
        public Thread[] RunJob { get; set; }
        public int Incr { get; set; }
        public bool SuccessExecuted { get; set; }

        public ViewModel()
        {
            Setup = new SetupProgramFile();
            GP = new GeneralParam();
            IJ = new InitializeJob();
            Verif = new Verification();
            SR = new SoftRunning();
            MThread = new ManageThread();
            Jobs = IJ.InitializeJobs();
            Incr = 0;
            RunJob = new Thread[IJ.NumberOfJobs()];

        }

        public string[][] DisplayJobs()
        {
            return new DisplayJobs().DisplayJob(Jobs);
        }

        public bool CreateJob(string name, string pathS, string pathD, bool differantial)
        {
            Job createJob = new Job(name, pathS, pathD, differantial);
            ActionJob create = new ActionJob(createJob);
            if (create.CreateJob())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ModifyJob(int _choice, string name, string pathS, string pathD, bool differantial)
        {
            ActionJob modify = new ActionJob(Jobs[_choice - 1]);
            if (modify.ModifyJob(name, pathS, pathD, differantial))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExecuteJob(int _choice)
        {
            ActionJob execute = new ActionJob(Jobs[_choice - 1]);
            if (execute.ExecuteJob())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool ExecuteAllJob()
        {
            SuccessExecuted = false;
            //TODO : MultiThread Parallal Job
            ActionJob[] execute = new ActionJob[IJ.NumberOfJobs()];
            for (int i = 0; i < execute.Length; i++)
            {
                execute[i] = new ActionJob(Jobs[i]);
            }

            foreach (ActionJob item in execute)
            {
                RunJob[Incr] = new Thread(() =>
                {
                    if (item.ExecuteJob())
                    {
                        SuccessExecuted = true;
                    }
                    else
                    {
                        SuccessExecuted = false;
                    }
                });
                Incr++;

            }
            foreach (Thread thread in RunJob)
            {
                thread.Start();
                thread.Join();
            }

            return SuccessExecuted;
        }

        public bool DeleteJob(int _choice)
        {
            ActionJob delete = new ActionJob(Jobs[_choice - 1]);
            if (delete.DeleteJob())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool VerifInputUser(string NameJob, string SourceJob, string DestinationJob, bool TypeBackupJob)
        {
            return Verif.VerifInputUser(NameJob, SourceJob, DestinationJob, TypeBackupJob);
        }


    }
}
