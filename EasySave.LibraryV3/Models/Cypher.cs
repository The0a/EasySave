using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace EasySave_V3Library.Models
{
    public class Cypher
    {
        /* public long Cyphering(string filePath)
         {
             string text = "";
             string key = "ezfozbfhzgbfyeozfbzeyfobezfyivtljhzpuhaydvazevpiahzdfoay";
             foreach (string line in File.ReadLines(filePath))
             {
                 text += line;
             }

             Stopwatch stopWatch = new Stopwatch();
             stopWatch.Start();
             try
             {
                 var result = new StringBuilder();
                 for (int c = 0; c < text.Length; c++)
                 {
                     result.Append((char)((uint)text[c] ^ (uint)key[c % key.Length]));
                 }
                 File.WriteAllText(filePath, result.ToString());

             }
             catch (Exception)
             {
                 stopWatch.Stop();
                 return -1;
             }
             stopWatch.Stop();
             return stopWatch.ElapsedMilliseconds + 1;
         }*/

        public long Cyphering(string filePath)
        {
            string initialBin;
            string crypted;
            string cryptedMessage;
            string data = "";

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {

                foreach (string line in File.ReadLines(filePath))
                {
                    data += line;
                }

                initialBin = StringToBin(data);
                string cléCrypted = StringToBin("Maxime");
                crypted = CryptageXOR(initialBin, cléCrypted);
                cryptedMessage = BinaryToString(crypted);
                File.WriteAllText(filePath, cryptedMessage);
            }
            catch (Exception)
            {
                stopWatch.Stop();
                return -1;
            }
            stopWatch.Stop();
            return stopWatch.ElapsedMilliseconds + 1;
        }



        public string StringToBin(string _data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in _data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        public string CryptageXOR(string _data, string _clé)
        {
            int sizeKey = _clé.Length;
            int incr = 0;
            char crypted;
            string cryptedData = "";
            foreach (char bin in _data)
            {
                if ((bin == '0' && _clé[incr % (sizeKey - 1)] == '0') || (bin == '1' && _clé[incr % (sizeKey - 1)] == '1'))
                {
                    crypted = '0';
                    cryptedData += crypted;
                }
                else
                {
                    crypted = '1';
                    cryptedData += crypted;
                }
                incr++;
            }
            return cryptedData;
        }

        public string BinaryToString(string _data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < _data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(_data.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }
    }
}

