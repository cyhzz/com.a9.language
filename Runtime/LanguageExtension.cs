using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.A9.Language
{
    public static class LanguageExtension
    {
        public static string GetLang(this string st, Dictionary<Com.A9.Language.Language, Dictionary<string, string>> dictionary, Com.A9.Language.Language lg)
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
                Debug.LogWarning(st + " is not in dictionary");
                return st;
            }
            return result;
        }
        public static string Localize(this string st, string dic_type = "Common")
        {
            if (st == null)
                return "error";
            if (CommonLanguage.dics.ContainsKey(dic_type))
                return st.GetLang(CommonLanguage.dics[dic_type], CommonLanguage.language);
            else
            {
                Debug.Log("Dictionary " + dic_type.ToString() + " is not loaded");
                return st;
            }
        }
    }
}

