using System.Collections;
using System.Collections.Generic;
using Com.A9.Language;
using UnityEngine;

public class CommonTitle : MonoBehaviour
{
    public List<GameObject> objs;
    void Start()
    {
        ChangeText();
        LangCtrl.OnLangChange += ChangeText;
    }
    private void OnDestroy()
    {
        LangCtrl.OnLangChange -= ChangeText;
    }
    public void ChangeText()
    {
        objs.ForEach(c => c.gameObject.SetActive(false));
        objs[(int)CommonLanguage.language].SetActive(true);
    }
}
