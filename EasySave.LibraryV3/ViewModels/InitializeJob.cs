using EasySave_V3Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave_V3.Library.ViewModels
{
    public class InitializeJob
    {
        public Job[] InitializeJobs()
        {
            string path = Directory.GetCurrentDirectory() + "/jobs";
            string[] files = Directory.GetFiles(path);
            int size = NumberOfJobs();
            Job[] jobs = new Job[size];
            int i = 0;
            int j = 0;


            foreach (var file in files)
            {
                string contentFile = File.ReadAllText(file);
                dynamic array = JsonConvert.DeserializeObject(contentFile);
                string[] info = new string[4];

                foreach (var item in array)
                {
                    info[j] = item.Value;
                    j++;
                }
                j = 0;
                jobs[i] = new Job(info[0], info[1], info[2], Boolean.Parse(info[3]));
                i++;

            }
            return jobs;
        }

        public int NumberOfJobs()
        {
            string path = Directory.GetCurrentDirectory() + "/jobs";
            string[] files = Directory.GetFiles(path);
            int size = files.Length;
            return size;
        }
    }
}
