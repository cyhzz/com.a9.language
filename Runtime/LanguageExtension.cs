using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.A9.Language
{
    public static class LanguageExtension
    {
        public static string GetLang(this string st, Dictionary<Language, Dictionary<string, string>> dictionary, Language lg)
        {
            if (dictionary == null)
            {
                Debug.Log("there is no dictionary");
                return st;
            }
            string result = null;
            Dictionary<string, string> grammar = null;
            dictionary.TryGetValue(lg, out grammar);
            //Load current dictioinary value into skills
            if (grammar != null)
                grammar.TryGetValue(st, out result);

            if (result == null)
            {
#if UNITY_EDITOR
                Debug.LogWarning(st + " is not in dictionary");
#endif
                return st;
            }
            return result;
        }

        public static string Localize(this string st, string dic_type = "Common")
        {
            if (st == null)
                return "error";

            if (CommonLanguage.LoadByXml)
            {
                if (CommonLanguage.xml_dics.ContainsKey(CommonLanguage.language))
                {
                    var db = CommonLanguage.xml_dics[CommonLanguage.language];
                    if (db.ContainsKey(st))
                    {
                        return db[st].Replace("<br>", "\n");
                    }
                    else
                    {
                        return st.Replace("<br>", "\n");
                    }
                }
                else
                {
                    return st;
                }
            }
            else
            {
                if (CommonLanguage.dics.ContainsKey(dic_type))
                {
                    var str = st.GetLang(CommonLanguage.dics[dic_type], CommonLanguage.language);
                    return str.Replace("<br>", "\n");
                }
                else
                {
                    Debug.Log("Dictionary " + dic_type.ToString() + " is not loaded");
                    return st;
                }
            }
        }

        public static string CapitalCHS(this string st, int start_size)
        {
            if (CommonLanguage.language != Language.CHS)
            {
                return st;
            }

            char first = st[0];
            int i = 0;
            if (first == '<')
            {
                for (i = 0; i < st.Length; i++)
                {
                    if (st[i] == '>')
                    {
                        i = i + 1;
                        first = st[i];
                        break;
                    }
                }
            }
            st = st.Remove(i, 1);
            st = st.Insert(i, $"<size={start_size + 10}>{first}</size>");
            return st;
        }
    }
}

