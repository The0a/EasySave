using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave_V3Library.Models
{
    public class Job
    {
        public string Name { get; set; }
        public string SourceFolder { get; set; }
        public string DestinationFolder { get; set; }
        public bool IsDifferantial { get; set; }

        public Job(string SaveName, string SaveSource, string SaveDestination, bool SaveIsDifferential)
        {
            Name = SaveName;
            SourceFolder = SaveSource;
            DestinationFolder = SaveDestination;
            IsDifferantial = SaveIsDifferential;
        }

        public bool CreateFileJob()
        {
            DestinationFolder = DestinationFolder + "\\" + Name + "_" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            Dictionary<string, object> opts = new Dictionary<string, object>
            {
                { "Jobname", Name },
                { "Path_Source", SourceFolder },
                { "Path_Destination", DestinationFolder },
                { "Type_Backup", IsDifferantial }
            };
            string stringjson = JsonConvert.SerializeObject(opts, Formatting.Indented); //All Information of the Job convert in JSON
            string filename = "jobs/" + Name + ".json"; //Json file name
            if (File.Exists(filename))
            {
                return false;
            }
            else
            {
                try
                {
                    File.WriteAllText(filename, stringjson); //Stock Job in a file
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
        }

        public bool ModifyJob(string nameJob, string pathS, string pathD, bool typeBackup)
        {
            if (DeleteJob())
            {
                Name = nameJob;
                SourceFolder = pathS;
                DestinationFolder = pathD;
                IsDifferantial = typeBackup;

                return CreateFileJob();
            }
            else
            {
                return false;
            }
        }

        public bool DeleteJob()
        {
            string filename = "jobs/" + Name + ".json";
            try
            {
                File.Delete(filename);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExecuteJob()
        {
            Backup save = new Backup(this);
            if (IsDifferantial)
            {
                if (save.Save(TypeBackup.Differantial))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (save.Save(TypeBackup.Complete))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
