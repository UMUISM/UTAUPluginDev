using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace UTAUPluginDev
{
    public class LangPack
    {
        private Dictionary<int, string> langData = new Dictionary<int, string>();
        private Dictionary<int, string> originLang;
        private XmlDocument conf = new XmlDocument();
        private bool useLangFile = false;
        private string nowLangName = "";

        public LangPack(string languageFile,string lang)
        {
            if(File.Exists(languageFile))
            {
                conf.Load(languageFile);

                nowLangName = lang;

                loadConfig();

                if (langData.Count != 0)
                {
                    useLangFile = true;
                }
            }
            else
            {
                MessageBox.Show("语言包文件：lang.xml找不到，请检查文件！\nLanguage package file: lang.xml can not be found, please check the file!\n言語パッケージファイル：lang.xmlが見つかりません。ファイルを確認してください。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.GetCurrentProcess().Kill();
            }
        }

        private void loadConfig()
        {
            XmlNodeList root = conf.GetElementsByTagName("languages");
            if (root.Count > 0)
            {
                XmlNodeList languages = ((XmlElement)root[0]).GetElementsByTagName("lang");
                foreach (XmlElement lang in languages)
                {
                    string langName = lang.GetAttribute("name");
                    Dictionary<int, string> tempList = new Dictionary<int, string>();
                    XmlNodeList sentences = lang.GetElementsByTagName("p");
                    foreach (XmlElement sentence in sentences)
                    {
                        tempList.Add(Convert.ToInt32(sentence.GetAttribute("id")), sentence.InnerText);
                    }
                    if (langName == "original")
                    {
                        originLang = tempList;
                    }
                    else
                    {
                        string[] nameList = langName.Split(',');
                        if (nameList.Contains(this.nowLangName))
                        {
                            langData = tempList;
                        }
                    }
                }
            }
        }

        public string fetch(string origin)
        {
            int id = originLang.FirstOrDefault(q => q.Value == origin).Key;  //get first key
            if (id != 0 && useLangFile && langData.ContainsKey(id))
            {
                return langData[id];
            }
            else
            {
                return origin;
            }
        }
    }
}
