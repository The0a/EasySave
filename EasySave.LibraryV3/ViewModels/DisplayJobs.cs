using EasySave_V3Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave_V3Library.ViewModels
{
    class DisplayJobs
    {
        public string[][] DisplayJob(Job[] Jobs)
        {
            string[][] jobs = InitDoubleArray(Jobs.Length, 4);

            int i = 0;
            foreach (Job job in Jobs)
            {
                jobs[i][0] = job.Name;
                jobs[i][1] = job.SourceFolder;
                jobs[i][2] = job.DestinationFolder;
                jobs[i][3] = job.IsDifferantial.ToString();
                i++;
            }

            return jobs;
        }

        private string[][] InitDoubleArray(int sizeOne, int sizeTwo)
        {
            string[][] _jobs = new string[sizeOne][];
            for (int j = 0; j < _jobs.Length; j++)
            {
                _jobs[j] = new string[sizeTwo];
            }
            return _jobs;
        }
    }
}
