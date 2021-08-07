using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace EasySave_V3Library.Models
{
    public class SetupProgramFile
    {
        public SetupProgramFile()
        {
            string pathJob = Directory.GetCurrentDirectory() + "/jobs";
            string pathLog = Directory.GetCurrentDirectory() + "/logs";
            Directory.CreateDirectory(pathJob);
            Directory.CreateDirectory(pathLog);
        }

        public void LogStateInit()
        {
            string logpath = "logs/log_" + DateTime.Today.ToString("dd_MM_yyyy") + ".json";
            string statepath = "logs/state.json";

            while (!CreateFileLog(logpath))
            { //I just need to recall the function
            }
            while (!CreateFileLog(statepath))
            { //I just need to recall the function 
            }
        }

        public bool CreateFileLog(string _path)
        {
            if (File.Exists(_path))
            {
                try
                {
                    string contfileLog = File.ReadAllText(_path);
                    contfileLog = Regex.Replace(contfileLog, "]", ",\n");
                    File.WriteAllText(_path, contfileLog);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            else
            {
                try
                {
                    File.Create(_path);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public void ManageLogFile(string jobName, string filePathS, string filePathD, string sizeFile, string timeCopy, long timeEncryption)
        {
            string today = DateTime.Today.ToString("dd_MM_yyyy");
            string now = DateTime.UtcNow.AddHours(1).ToString("HH:mm:ss");

            Dictionary<string, object> opts = new Dictionary<string, object>
            {
                { "Date", now },
                { "Jobname", jobName },
                { "Path_Source", filePathS },
                { "Path_Destination", filePathD },
                { "Size_of_the_file", sizeFile },
                { "Time_to_Copy", timeCopy }
            };

            if (timeEncryption == 0)
            {
                opts.Add("Time_to_Cypher", "No encryption");
            }
            else if (timeEncryption > 0)
            {
                opts.Add("Time_to_Cypher", timeEncryption.ToString() + "ms");

            }
            else
            {
                opts.Add("Time_to_Cypher", "error");
            }


            string stringjson = JsonConvert.SerializeObject(opts, Formatting.Indented); //All Information of the Job convert in JSON

            string logpath = "logs/log_" + today + ".json";

            if (!File.Exists(logpath))
            {
                File.AppendAllText(logpath, "[\n");
            }
            File.AppendAllText(logpath, stringjson);
            File.AppendAllText(logpath, ",\n\n");


        }

        public void ManageStateFile(string jobName, string filePathS, string filePathD, string sizeFile, string timeCopy, string fileName, int nbFileCopied)
        {
            string time = DateTime.UtcNow.AddHours(1).ToString("HH:mm:ss");
            int numberFileStart = Directory.GetFiles(filePathS, "*", SearchOption.AllDirectories).Length;
            int numberFileFinish = nbFileCopied + 1;
            int numberFileRemaining = numberFileStart - numberFileFinish;
            float pourcentageFini = ((float)((double)numberFileFinish / numberFileStart)) * 100;

            Dictionary<string, object> opts = new Dictionary<string, object>
            {
                { "Date", time },
                { "Jobname", jobName },
                { "Souce_Path", filePathS },
                { "Destination_Path", filePathD },
                { "Filename", fileName },
                { "Number_File_Source", numberFileStart },
                { "Number_File_Destination", numberFileFinish },
                { "Number_File_Remaining", numberFileRemaining },
                { "PercentFinish", pourcentageFini },
                { "Size_File", sizeFile },
                { "Time_to_Copy", timeCopy }
            };
            string stringjson = JsonConvert.SerializeObject(opts, Formatting.Indented); //All Information of the Job convert in JSON

            string logpath = "logs/state.json";


            if (!File.Exists(logpath))
            {
                File.AppendAllText(logpath, "[\n");
            }
            File.AppendAllText(logpath, stringjson);
            File.AppendAllText(logpath, ",\n\n");

        }

        public void CloseJsonFile()
        {
            string logpath = "logs/log_" + DateTime.Today.ToString("dd_MM_yyyy") + ".json";
            string statepath = "logs/state.json";

            CloseFile(logpath);

            CloseFile(statepath);
        }

        private void CloseFile(string _path)
        {
            string s = File.ReadAllText(_path);
            int index = s.Length - 3;
            s = s.Substring(0, index);

            File.WriteAllText(_path, s + "\n]");


        }

    }
}
