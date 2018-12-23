using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UTAUPluginDev
{
    public class UstToClass
    {        
        public UstToClass(string path, ref ustFile ust)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = sr.ReadToEnd();

                    if (line.Contains("UstVersion=")==true)
                    {                        
                        ust.Version = Convert.ToDouble(line.Substring(line.IndexOf("=")+1, line.Length-1));
                    }

                    if (line.Contains("Tempo=") == true)
                    {
                        ust.Tempo = Convert.ToDouble(line.Substring(line.IndexOf("=")+1, line.Length - 1));
                    }

                    if (line.Contains("ProjectName=") == true)
                    {
                        ust.PjName = line.Substring(line.IndexOf("=")+1, line.Length - 1);
                    }

                    if (line.Contains("VoiceDir=") == true)
                    {
                        ust.VoicePath = line.Substring(line.IndexOf("=")+1, line.Length - 1);
                    }

                    if (line.Contains("OutFile=") == true)
                    {
                        ust.OutFile = line.Substring(line.IndexOf("=") + 1, line.Length - 1);
                    }

                    if (line.Contains("CacheDir=") == true)
                    {
                        ust.Cache = line.Substring(line.IndexOf("=") + 1, line.Length - 1);
                    }

                    if (line.Contains("Tool1=") == true)
                    {
                        ust.Tool1 = line.Substring(line.IndexOf("=") + 1, line.Length - 1);
                    }

                    if (line.Contains("Tool2=") == true)
                    {
                        ust.Tool2 = line.Substring(line.IndexOf("=") + 1, line.Length - 1);
                    }

                    if (line.Contains("Flags=") == true)
                    {
                        ust.Flags = line.Substring(line.IndexOf("=") + 1, line.Length - 1);
                    }                    

                    if (line.Contains("Mode2=") == true)
                    {
                        ust.Mode2 = Convert.ToBoolean(line.Substring(line.IndexOf("=") + 1, line.Length - 1));
                    }

                    if (line.Contains("Autoren=") == true)
                    {
                        ust.Autoren = Convert.ToBoolean(line.Substring(line.IndexOf("=") + 1, line.Length - 1));
                    }

                    if (line.Contains("MapFirst=") == true)
                    {
                        ust.MapFirst = Convert.ToBoolean(line.Substring(line.IndexOf("=") + 1, line.Length - 1));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("错误的UST文件" + e.Message);
            }
        }
    }
}
