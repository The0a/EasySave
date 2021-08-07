using EasySave.LibraryV3.Models;
using EasySave_V3.Library.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace EasySave_V3Library.Models
{

    public enum TypeBackup
    {
        Differantial,
        Complete
    }


    public class Backup
    {
        public Job JobToBackup { get; set; }
        public Cypher Cyphering { get; set; }
        public SetupProgramFile SPF { get; set; }
        public GeneralParam GP { get; set; }
        public string InitialFolderSave { get; set; }
        public ManageThread TManager { get; set; }

        public Backup(Job job)
        {
            JobToBackup = job;
            InitialFolderSave = JobToBackup.DestinationFolder + "\\Save_0";
            Cyphering = new Cypher();
            GP = new GeneralParam();
            SPF = new SetupProgramFile();
            TManager = new ManageThread();
        }

        public bool Save(TypeBackup typeBackup)
        {

            if (typeBackup == TypeBackup.Complete)
            {

                if (CompleteSave())
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
                //Differantial Save
                if (DifferentialSave())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool DifferentialSave()
        {
            InitializeFolder();
            if (Directory.GetDirectories(JobToBackup.DestinationFolder).Length == 0)
            {
                try
                {
                    // 0 Save Before
                    InitialSaveDiff();
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
                    // Already Save
                    int numberSave = Directory.GetDirectories(JobToBackup.DestinationFolder).Length;
                    JobToBackup.DestinationFolder += "\\Save_" + numberSave;
                    Directory.CreateDirectory(JobToBackup.DestinationFolder);

                    TManager.InitiallizePauseSoft(GP.FileContent["SoftWork"]);
                    Decrypte();
                    FolderCopy();
                    Analyse();
                    Decrypte();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }


        private void Analyse()
        {
            bool isDownOneTime = false;
            int k = 0;

            foreach (var sourcefile in Directory.GetFiles(JobToBackup.SourceFolder, "*", SearchOption.AllDirectories))
            {
                foreach (var destfile in Directory.GetFiles(InitialFolderSave, "*", SearchOption.AllDirectories))
                {
                    //TODO : Pause SOftRUnning
                    PauseSoft();

                    string pathToTest = PathToTest(sourcefile);

                    if (new FileInfo(sourcefile).Name == new FileInfo(destfile).Name)
                    {
                        if (FileEquals(sourcefile, destfile))
                        {
                            //Fichier Identique pas de copy
                        }
                        else
                        {
                            //Store File Info in an array
                            Copy(sourcefile, k);
                            k++;
                        }
                        break;
                    }

                    else if (!File.Exists(pathToTest) && !isDownOneTime)
                    {
                        //New File
                        Copy(sourcefile, k);
                        k++;
                        isDownOneTime = true;
                    }

                }
                isDownOneTime = false;
            }
            TManager.EndCopy = true;
        }

        private void PauseSoft()
        {
            if (TManager.IsWorkSoftRunning)
            {
                TManager.PauseSoft();
                while (TManager.IsWorkSoftRunning)
                {
                    Thread.Sleep(100);
                }
                TManager.RunSoft();
            }
            else
            {
                TManager.RunSoft();
            }
        }

        private string PathToTest(string _sourcefile)
        {
            string relative = Path.GetRelativePath(JobToBackup.SourceFolder, Path.GetDirectoryName(_sourcefile));
            if (relative == ".")
            {
                relative = "\\";
            }
            else
            {
                relative = "\\" + relative + "\\";
            }

            return InitialFolderSave + relative + new FileInfo(_sourcefile).Name;
        }

        private bool FileEquals(string path1, string path2)
        {
            string[] file1 = File.ReadAllLines(path1);
            string[] file2 = File.ReadAllLines(path2);
            if (file1.Length == file2.Length)
            {
                for (int i = 0; i < file1.Length; i++)
                {
                    if (file1[i] != file2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }


        public void TestFile(string filename, List<string> list)
        {
            TextWriter tw = new StreamWriter(filename);

            foreach (String s in list)
                tw.WriteLine(s);

            tw.Close();
        }

        private void InitialSaveDiff()
        {
            JobToBackup.DestinationFolder += "\\Save_0";
            Directory.CreateDirectory(JobToBackup.DestinationFolder);
            while (!CompleteSave())
            {
                //Rerun CompleteSave
            }
        }

        private bool CompleteSave()
        {
            string[] pathSourceFiles = Directory.GetFiles(JobToBackup.SourceFolder, "*", SearchOption.AllDirectories);
            int i = 0;
            TManager.InitiallizePauseSoft(GP.FileContent["SoftWork"]);

            InitializeFolder();
            FolderCopy();

            foreach (string file in pathSourceFiles)
            {
                //TODO : Pause SOftRUnning
                PauseSoft();
                try
                {
                    Copy(file, i);
                    i++;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            TManager.EndCopy = true;
            return true;
        }

        private void Copy(string _file, int numberOfFile)
        {
            string filePathS = Path.GetDirectoryName(_file);

            string durationCopy = CopyWTime(_file, JobToBackup.SourceFolder, JobToBackup.DestinationFolder);

            string pathDestinationFile = Directory.GetFiles(JobToBackup.DestinationFolder, "*", SearchOption.AllDirectories)[numberOfFile];
            long durationCypher = DurationCryptage(pathDestinationFile);

            string fileSize = ((new FileInfo(_file).Length) / 1048576).ToString() + "Mo";
            LogStateFileGestion(JobToBackup.Name, filePathS, pathDestinationFile, fileSize, durationCopy, durationCypher, new FileInfo(_file).Name, numberOfFile);
        }


        private void Decrypte()
        {
            long durationCypher;
            foreach (var destfile in Directory.GetFiles(InitialFolderSave, "*", SearchOption.AllDirectories))
            {
                durationCypher = DurationCryptage(destfile);
            }
        }

        private long DurationCryptage(string destfile)
        {
            string extension = "." + GP.FileContent["Extension"];
            if (extension == new FileInfo(destfile).Extension)
            {
                //Verify if the file is crypted
                return Cyphering.Cyphering(destfile);
            }
            return 0;
        }


        private void LogStateFileGestion(string name, string source, string dest, string filesize, string durationcopy, long durationcypher, string filename, int numberfilecopied)
        {
            SPF.ManageLogFile(name, source, dest, filesize, durationcopy, durationcypher);

            SPF.ManageStateFile(name, source, dest, filesize, durationcopy, filename, numberfilecopied);

            SPF.CloseJsonFile();
        }


        private void InitializeFolder()
        {
            Directory.CreateDirectory(JobToBackup.DestinationFolder);
        }

        private void FolderCopy()
        {
            string[] pathSourceDirectories = Directory.GetDirectories(JobToBackup.SourceFolder, "*", SearchOption.AllDirectories);

            foreach (var dir in pathSourceDirectories)
            {
                Directory.CreateDirectory(dir.Replace(JobToBackup.SourceFolder, JobToBackup.DestinationFolder));
            }
        }

        public string CopyWTime(string file, string _source, string _destination)
        {
            Stopwatch chrono = new Stopwatch();
            chrono.Start();
            try
            {
                File.Copy(file, file.Replace(_source, _destination), true);
            }
            catch (Exception)
            {

                chrono.Stop();
                return "-1ms";
            }
            chrono.Stop();
            return chrono.ElapsedMilliseconds.ToString() + "ms";
        }

    }
}