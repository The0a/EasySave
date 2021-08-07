using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave_V3.Library.ViewModels
{
    public class Verification
    {
        public bool VerifInputUser(string name, string pathS, string pathD, bool type)
        {
            char[] specialChar = Path.GetInvalidFileNameChars();
            foreach (var item in specialChar)
            {
                if (name.Contains(item))
                {
                    return false;
                }
            }

            if (name.Length < 1 || name.Length > 30)
            {
                return false;
            }

            else if (!Directory.Exists(pathS))
            {
                return false;
            }

            else if (!Directory.Exists(pathD))
            {
                return false;
            }

            else if (type != false && type != true)
            {
                return false;
            }

            else
            {
                return true;
            }
        }
    }
}
