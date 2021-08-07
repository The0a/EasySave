using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave.WPF.Langages
{
    public enum Langages
    {
        French,
        English
    }
    public class Langage
    {
        public string Langue { get; set; }
        public Langages LangueChoice { get; set; }
        public Langage(Langages langue)
        {
            switch (langue)
            {
                case Langages.French:
                    Langue = "French";
                    LangueChoice = Langages.French;
                    break;
                case Langages.English:
                    Langue = "English";
                    LangueChoice = Langages.English;
                    break;
                default:
                    break;
            }
        }

        public string DisJobs()
        {
            string fr = "Afficher les jobs";
            string en = "Display Jobs";
            switch (LangueChoice)
            {
                case Langages.French:
                    return fr;
                case Langages.English:
                    return en;
                default:
                    return en;
            }
        }

        public string CreJobs()
        {
            string fr = "Créer un Job";
            string en = "Create a Job";
            switch (LangueChoice)
            {
                case Langages.French:
                    return fr;
                case Langages.English:
                    return en;
                default:
                    return en;
            }
        }

        public string ModJobs()
        {
            string fr = "Modifier un Job";
            string en = "Modify a Job";
            switch (LangueChoice)
            {
                case Langages.French:
                    return fr;
                case Langages.English:
                    return en;
                default:
                    return en;
            }
        }

        public string ExeJobs()
        {
            string fr = "Executer un Job";
            string en = "Execute a Job";
            switch (LangueChoice)
            {
                case Langages.French:
                    return fr;
                case Langages.English:
                    return en;
                default:
                    return en;


            }
        }

        public string DelJobs()
        {
            string fr = "Supprimer un Job";
            string en = "Delete a Job";
            switch (LangueChoice)
            {
                case Langages.French:
                    return fr;
                case Langages.English:
                    return en;
                default:
                    return en;
            }
        }

        public string GenPJobs()
        {
            string fr = "Paramètres généraux";
            string en = "General Parameters";
            switch (LangueChoice)
            {
                case Langages.French:
                    return fr;
                case Langages.English:
                    return en;
                default:
                    return en;
            }
        }
    }
}
