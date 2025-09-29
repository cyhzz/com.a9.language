using System.Collections;
using System.Collections.Generic;
using Com.A9.Language;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommonLabel : MonoBehaviour
{
    TMP_Text lb;
    Text lbt;

    void Start()
    {
        lb = GetComponent<TMP_Text>();
        lbt = GetComponent<Text>();
        LangCtrl.OnLangChange += ChangeText;
        ChangeText();
    }

    private void OnDestroy()
    {
        LangCtrl.OnLangChange -= ChangeText;
    }

    public void ChangeText()
    {
        if (lb)
        {
            lb.text = name.Localize();
        }
        if (lbt)
        {
            lbt.text = name.Localize();
        }
    }
}
