using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Com.A9.FileReader;
using LumenWorks.Framework.IO.Csv;
using UnityEngine;
namespace Com.A9.Language
{
    [System.Serializable]
    public class LanguageTableItem
    {
        public string id;
        public string EN;
        public string CHS;
    }

    [System.Serializable]
    public enum Language
    {
        EN, CHS
    }
    public class CommonLanguage
    {
        //list of dictionraies
        public static string path = "Language/";
        public static bool LoadByXml = false;
        public static bool recordArchive = false;

        public static Language language = Language.CHS;
        public static Dictionary<string, Dictionary<Language, Dictionary<string, string>>> dics
            = new Dictionary<string, Dictionary<Language, Dictionary<string, string>>>();

        public static Dictionary<Language, Dictionary<string, string>> xml_dics = new Dictionary<Language, Dictionary<string, string>>();
        public static List<string> queries = new List<string>();

        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            xml_dics.Clear();
            queries.Clear();
            dics.Clear();
            //dictionary = xmlReader.GetDictionary("Common/Lang");
            TextAsset[] txts = Resources.LoadAll<TextAsset>(path);
            for (int i = 0; i < txts.Length; i++)
            {
                Dictionary<Language, Dictionary<string, string>> new_dic = GetDictionary(path + txts[i].name);
                dics.Add(txts[i].name, new_dic);
            }

            xml_dics.Add(Language.EN, new Dictionary<string, string>());
            xml_dics.Add(Language.CHS, new Dictionary<string, string>());

            var list = Resources.Load<LanguageTable>("GameData/LanguageTable");
            foreach (var item in list.items)
            {
                xml_dics[Language.EN].Add(item.id, item.EN);
                xml_dics[Language.CHS].Add(item.id, item.CHS);
            }

#if UNITY_EDITOR
            List<string> que = new List<string>();
            xmlReader.ReadJson<List<string>>("LocalizeArchive.json", out que);
            if (que != null)
            {
                queries = que;
            }
#endif
        }
#if UNITY_EDITOR


        static void ExportQueries()
        {
            xmlReader.SaveAsJson("LocalizeArchive.json", queries);
        }

        public static void ExportUnusedColumns()
        {
            ExportQueries();
            List<string> unused = new List<string>();
            foreach (var item in xml_dics)
            {
                foreach (var s in item.Value)
                {
                    if (!queries.Contains(s.Key))
                    {
                        if (unused.Contains(s.Key) == false)
                        {
                            unused.Add(s.Key);
                        }
                    }
                }
            }
            xmlReader.SaveAsXml("UnusedColumns.xlsx", unused);
        }
#endif
        public static Dictionary<Language, Dictionary<string, string>> GetDictionary(string path)
        {
            Dictionary<Language, Dictionary<string, string>> dic = new Dictionary<Language, Dictionary<string, string>>();
            Dictionary<int, Language> colSpecifier = new Dictionary<int, Language>();
            TextAsset tx = Resources.Load<TextAsset>(path);
            CsvReader csv = new CsvReader(new StreamReader(new MemoryStream(tx.bytes), Encoding.UTF8), true);
            int fieldCount = csv.FieldCount;
            string[] headers = csv.GetFieldHeaders();
            List<string> languages = new List<string>(Enum.GetNames(typeof(Language)));
            for (int i = 0; i < headers.Length; i++)
            {
                if (languages.Contains(headers[i]))
                {
                    colSpecifier.Add(i, (Language)languages.IndexOf(headers[i]));
                    dic.Add((Language)languages.IndexOf(headers[i]), new Dictionary<string, string>());
                }
            }
            while (csv.ReadNextRecord())
            {
                for (int i = 0; i < fieldCount; i++)
                {
                    int rem = i % headers.Length;
                    if (rem != 0)
                    {
                        int lead = i - rem;
                        Language l = colSpecifier[rem];
                        Dictionary<string, string> currentGrammar = null;
                        dic.TryGetValue(l, out currentGrammar);
                        if (currentGrammar != null)
                            currentGrammar.Add(csv[lead], csv[i]);
                    }
                }
            }
            //foreach (var item in dic)
            //{
            //    Debug.Log(item.Key);
            //    foreach (var s in item.Value)
            //    {
            //        Debug.Log(s.Key+" :: "+s.Value);
            //    }
            //}
            return dic;
        }
    }
}

