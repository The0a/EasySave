using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave_V3.Library.Models
{
    public class GeneralParam
    {
        public string FilePath { get; set; }
        public Dictionary<string, string> FileContent { get; set; }
        public GeneralParam()
        {
            FileContent = new Dictionary<string, string>();
            InitializeFile();
        }

        public void InitializeFile()
        {
            FilePath = Directory.GetCurrentDirectory() + "/general_param/generalparam.json";
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/general_param");
            if (!File.Exists(FilePath))
            {
                FileContent.Add("SoftWork", "C:\\Program Files\\WindowsApps\\Microsoft.WindowsCalculator_10.2010.0.0_x64__8wekyb3d8bbwe\\Calculator.exe");
                FileContent.Add("Extension", "Nothing, Enter your extension here (without dot)");
                string forFile = JsonConvert.SerializeObject(FileContent, Formatting.Indented);
                File.WriteAllText(FilePath, forFile);
            }
            else
            {
                FileContent = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(FilePath));
            }
        }


        public void ModifySoftWork(string _processus)
        {
            FileContent["SoftWork"] = _processus;
            string content = JsonConvert.SerializeObject(FileContent, Formatting.Indented);
            File.WriteAllText(FilePath, content);
        }

        public void ModifyExtensionCypher(string _ext)
        {
            FileContent["Extension"] = _ext;
            string content = JsonConvert.SerializeObject(FileContent, Formatting.Indented);
            File.WriteAllText(FilePath, content);
        }
    }
}
