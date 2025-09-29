using System;
using System.Collections;
using System.Collections.Generic;
using Com.A9.DataConsistancy;
using Com.A9.Language;
using UnityEngine;

public class LangCtrl : MonoBehaviour
{
    public SimpleButtonCtrl lang;
    public static Action OnLangChange;

    void Start()
    {
        if (PlayerPrefsV2.GetInt("lang", 0) == 0)
        {
            PlayerPrefsV2.SetInt("lang", 2);
        }

        lang.OnValChange.AddListener(PresentLang);
        lang.SetValue(PlayerPrefsV2.GetInt("lang", 0) - 1, true);
    }

    public void PresentLang(int idx)
    {
        PlayerPrefsV2.SetInt("lang", idx + 1);
        Debug.Log(PlayerPrefsV2.GetInt("lang", 0));
        CommonLanguage.language = (Language)idx;
        ResourceManager.instance.SavePlayerData();
        OnLangChange?.Invoke();
    }
}
